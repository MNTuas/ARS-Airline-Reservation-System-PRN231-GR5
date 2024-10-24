using BusinessObjects.Models;
using BusinessObjects.RequestModels.Booking;
using FFilms.Application.Shared.Response;
using Microsoft.AspNetCore.Http;
using Repository.Repositories.BookingRepositories;
using Service.Enums;
using Service.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services.BookingServices
{
    public class BookingService : IBookingService
    {
        private readonly IBookingRepository _bookingRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public BookingService(IBookingRepository bookingRepository, IHttpContextAccessor httpContextAccessor)
        {
            _bookingRepository = bookingRepository;
            _httpContextAccessor = httpContextAccessor;
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
    }
}
