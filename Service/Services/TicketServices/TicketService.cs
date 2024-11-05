using AutoMapper;
using BusinessObjects.Models;
using BusinessObjects.RequestModels.Booking;
using BusinessObjects.RequestModels.Passenger;
using BusinessObjects.RequestModels.Ticket;
using FFilms.Application.Shared.Response;
using Microsoft.AspNetCore.Http;
using Repository.Repositories.AirporRepositories;
using Repository.Repositories.BookingRepositories;
using Repository.Repositories.PassengerRepositories;
using Repository.Repositories.TicketRepositories;
using Service.Enums;
using Service.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services.TicketServices
{
    public class TicketService : ITicketService
    {
        private readonly ITicketRepository _ticketRepository;
        private readonly IPassengerRepository _passengerRepository;
        private readonly IBookingRepository _bookingRepository;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public TicketService(ITicketRepository TicketRepository, IPassengerRepository passengerRepository,
                             IBookingRepository bookingRepository, IMapper mapper,
                             IHttpContextAccessor httpContextAccessor)
        {
            _ticketRepository = TicketRepository;
            _passengerRepository = passengerRepository;
            _bookingRepository = bookingRepository;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
        }



        public async Task<Result<List<Ticket>>> addTicket(List<CreateTicketRequest> createTicketRequest)
        {
            try
            {

                var idclaim = _httpContextAccessor.HttpContext.User.FindFirst(MySetting.CLAIM_USERID);
                var userid = idclaim.Value;
                var ticketList = new List<Ticket>();

                foreach (var ticket in createTicketRequest)
                {
                    var newTicket = new Ticket
                    {
                        Id = Guid.NewGuid().ToString(),
                        Country = ticket.Country,
                        Dob = ticket.Dob,
                        FirstName = ticket.FirstName,
                        LastName = ticket.LastName,
                        Gender = ticket.Gender,
                        Status = TicketStatusEnums.Pending.ToString(),
                        BookingId = ticket.BookingId,
                        TicketClassId = ticket.TicketClassId,
                    };
                    ticketList.Add(newTicket);
                }

                await _ticketRepository.InsertRange(ticketList);
                return new Result<List<Ticket>>
                {
                    Success = true,
                    Data = ticketList
                };

            }
            catch (Exception ex)
            {
                return new Result<List<Ticket>>
                {
                    Success = false,
                    Message = ex.Message,
                };
            }
        }
    }
}


//    public async Task<Result<List<Ticket>>> AddTicket(CreateBookingRequest createBookingRequest,
//List<CreatePassengerRequest> createPassengerRequests, CreateTicketRequest createTicketRequest)
//    {
//        try
//        {
//            var idclaim = _httpContextAccessor.HttpContext.User.FindFirst(MySetting.CLAIM_USERID);
//            var userid = idclaim.Value;

//            // Create a new booking
//            var newBooking = new Ticket
//            {
//                Id = Guid.NewGuid().ToString(),
//                CreatedDate = DateTime.Now,
//                Status = BookingStatusEnums.Pending.ToString(),
//                Quantity = createBookingRequest.Quantity,
//                UserId = userid,
//            };

//            await _bookingRepository.Insert(newBooking); 

//            var tickets = new List<Ticket>(); 


//            foreach (var createPassengerRequest in createPassengerRequests)
//            {

//                var newPassenger = new Passenger
//                {
//                    Id = Guid.NewGuid().ToString(),
//                    UserId = userid,
//                    Country = createPassengerRequest.Country,
//                    Dob = createPassengerRequest.Dob,
//                    FirstName = createPassengerRequest.FirstName,
//                    LastName = createPassengerRequest.LastName,
//                    Gender = createPassengerRequest.Gender,
//                    Type = createPassengerRequest.Type,
//                };

//                await _passengerRepository.Insert(newPassenger);


//                var newTicket = new Ticket
//                {
//                    Id = Guid.NewGuid().ToString(),
//                    BookingId = newBooking.Id,
//                    PassengerId = newPassenger.Id,
//                    TicketClassId = createTicketRequest.TicketClassId,
//                    Status = false,
//                };

//                await _ticketRepository.Insert(newTicket); 

//                // Add ticket to the list
//                tickets.Add(newTicket);
//            }


//            return new Result<List<Ticket>>
//            {
//                Success = true,
//                Message = "Create successfull!",
//                Data = tickets
//            };
//        }
//        catch (Exception ex)
//        {
//            return new Result<List<Ticket>>
//            {
//                Success = false,
//                Message = ex.Message,
//            };
//        }
//    }


