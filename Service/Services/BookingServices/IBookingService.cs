using BusinessObjects.Models;
using BusinessObjects.RequestModels.Booking;
using BusinessObjects.ResponseModels.Booking;
using FFilms.Application.Shared.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services.BookingServices
{
    public interface IBookingService
    {
        Task<Result<BookingInformation>> addBooking(CreateBookingRequest createBookingRequest);
        Task UpdateBookingStatus(string id, string status);
        Task<List<UserBookingResponseModel>> GetOwnBookings();
        Task<UserBookingResponseModel> GetBookingById(string id);
    }
}
