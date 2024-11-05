using BusinessObjects.Models;
using BusinessObjects.RequestModels.Booking;
using BusinessObjects.ResponseModels.Booking;
using FFilms.Application.Shared.Response;

namespace Service.Services.BookingServices
{
    public interface IBookingService
    {
        Task<List<BookingResponseModel>> GetAllBookings();
        Task<Result<BookingInformation>> addBooking(CreateBookingRequest createBookingRequest);
        Task UpdateBookingStatus(string id, string status);
        Task<List<UserBookingResponseModel>> GetOwnBookings();
        Task<UserBookingResponseModel> GetBookingById(string id);
        Task<string> AutoUpdateBookingStatus();
        Task CancelBooking(string id);
    }
}
