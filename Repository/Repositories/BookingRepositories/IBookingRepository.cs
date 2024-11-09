using BusinessObjects.Models;
using Repository.Repositories.GenericRepositories;

namespace Repository.Repositories.BookingRepositories
{
    public interface IBookingRepository : IGenericRepository<BookingInformation>
    {
        Task<List<BookingInformation>> GetAllBooking();
        Task<List<BookingInformation>> GetAllBookingOfUser(string userId);
        Task<BookingInformation> GetById(string id);
        Task<decimal> GetTotalPriceOfBooking(string id);
        Task<List<BookingInformation>> GetAllPendingBookings();
        Task<List<BookingInformation>> GetAllRefundBooking();
    }
}
