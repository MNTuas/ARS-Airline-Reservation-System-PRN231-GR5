using AutoMapper;
using BusinessObjects.Models;
using BusinessObjects.RequestModels.Booking;
using BusinessObjects.ResponseModels.Booking;
using FFilms.Application.Shared.Response;
using Microsoft.AspNetCore.Http;
using Repository.Enums;
using Repository.Repositories.BookingRepositories;
using Repository.Repositories.TicketClassRepositories;
using Service.Enums;
using Service.Helper;

namespace Service.Services.BookingServices
{
    public class BookingService : IBookingService
    {
        private readonly IBookingRepository _bookingRepository;
        private readonly ITicketClassRepository _ticketClassRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IMapper _mapper;

        public BookingService(IBookingRepository bookingRepository, IHttpContextAccessor httpContextAccessor, IMapper mapper, ITicketClassRepository ticketClassRepository)
        {
            _bookingRepository = bookingRepository;
            _httpContextAccessor = httpContextAccessor;
            _mapper = mapper;
            _ticketClassRepository = ticketClassRepository;
        }

        public async Task<List<BookingResponseModel>> GetAllBookings()
        {
            var list = await _bookingRepository.GetAllBooking();
            return _mapper.Map<List<BookingResponseModel>>(list);
        }

        public async Task<Result<BookingInformation>> addBooking(CreateBookingRequest createBookingRequest)
        {
            try
            {

                var idclaim = _httpContextAccessor.HttpContext.User.FindFirst(MySetting.CLAIM_USERID);
                var userid = idclaim.Value;


                var newBooking = new BookingInformation
                {
                    Id = Guid.NewGuid().ToString(),
                    CreatedDate = DateTime.Now,
                    Status = BookingStatusEnums.Pending.ToString(),
                    Quantity = createBookingRequest.Quantity,
                    UserId = userid,
                };

                await _bookingRepository.Insert(newBooking);
                return new Result<BookingInformation>
                {
                    Success = true,
                    Data = newBooking
                };

            }
            catch (Exception ex)
            {
                return new Result<BookingInformation>
                {
                    Success = false,
                    Message = ex.Message,
                };
            }
        }

        public async Task UpdateBookingStatus(string id, string status)
        {
            var booking = await _bookingRepository.GetById(id);
            if (booking == null)
            {
                throw new Exception("Not found booking");
            }
            booking.Status = status;
            foreach (var ticket in booking.Tickets)
            {
                ticket.Status = status;
            }
            await _bookingRepository.Update(booking);
        }

        public async Task<List<UserBookingResponseModel>> GetOwnBookings()
        {
            var idclaim = _httpContextAccessor.HttpContext.User.FindFirst(MySetting.CLAIM_USERID);
            var userid = idclaim.Value;
            var list = await _bookingRepository.GetAllBookingOfUser(userid);
            return _mapper.Map<List<UserBookingResponseModel>>(list);
        }

        public async Task<UserBookingResponseModel> GetBookingById(string id)
        {
            var booking = await _bookingRepository.GetById(id);
            return _mapper.Map<UserBookingResponseModel>(booking);
        }

        public async Task<string> AutoUpdateBookingStatus()
        {
            var bookings = await _bookingRepository.GetAllPendingBookings();
            var updateList = new List<BookingInformation>();
            var updateTicketClassList = new List<TicketClass>();
            foreach (var booking in bookings)
            {
                if (DateTime.Now.Subtract(booking.CreatedDate).TotalMinutes >= 15)
                {
                    updateList.Add(booking);
                }
            }
            foreach (var booking in updateList)
            {
                booking.Status = BookingStatusEnums.Cancelled.ToString();
                foreach (var ticket in booking.Tickets)
                {
                    ticket.Status = BookingStatusEnums.Cancelled.ToString();
                }
                var ticketClassId = booking.Tickets.FirstOrDefault().TicketClassId;
                var quantity = booking.Tickets.Count();
                var ticketClass = await _ticketClassRepository.GetTicketClassById(ticketClassId);
                ticketClass.RemainSeat += quantity;
                updateTicketClassList.Add(ticketClass);
            }

            await _ticketClassRepository.UpdateRange(updateTicketClassList);
            await _bookingRepository.UpdateRange(updateList);
            return "Booking update successfully";
        }

        public async Task CancelBooking(string id)
        {
            var booking = await _bookingRepository.GetById(id);
            if (booking.Status.Equals(BookingStatusEnums.Cancelled.ToString()))
            {
                throw new Exception("This booking is already cancelled");
            }

            if (booking.Tickets.FirstOrDefault().TicketClass.Flight.Status.Equals(FlightStatusEnums.Arrived.ToString()))
            {
                throw new Exception("This flight is already arrived.");
            }

            booking.Status = BookingStatusEnums.Cancelled.ToString();
            booking.IsRefund = false;
            booking.CancelDate = DateTime.Now;

            foreach (var ticket in booking.Tickets)
            {
                ticket.Status = BookingStatusEnums.Cancelled.ToString();
            }

            var ticketClassId = booking.Tickets.FirstOrDefault().TicketClassId;
            var quantity = booking.Tickets.Count();
            var ticketClass = await _ticketClassRepository.GetTicketClassById(ticketClassId);
            ticketClass.RemainSeat += quantity;

            await _ticketClassRepository.Update(ticketClass);
            await _bookingRepository.Update(booking);
        }

        public async Task<List<BookingResponseModel>> GetAllRefundBooking()
        {
            var list = await _bookingRepository.GetAllRefundBooking();
            return _mapper.Map<List<BookingResponseModel>>(list);
        }

        public async Task UpdateRefundBooking(string id)
        {
            var booking = await _bookingRepository.GetById(id);
            if (booking == null)
            {
                throw new Exception("Booking not found!");
            }
            booking.IsRefund = true;
            await _bookingRepository.Update(booking);
        }
    }
}
