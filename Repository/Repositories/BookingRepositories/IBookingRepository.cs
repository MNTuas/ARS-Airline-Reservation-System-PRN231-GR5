using BusinessObjects.Models;
using Repository.Repositories.GenericRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repositories.BookingRepositories
{
    public interface IBookingRepository : IGenericRepository<BookingInformation>
    {
        Task<List<BookingInformation>> GetAllBooking();
        Task<List<BookingInformation>> GetAllBookingOfUser(string userId);
        Task<BookingInformation> GetById(string id);
        Task<decimal> GetTotalPriceOfBooking(string id);
        Task<List<BookingInformation>> GetAllPendingBookings();
    }
}
