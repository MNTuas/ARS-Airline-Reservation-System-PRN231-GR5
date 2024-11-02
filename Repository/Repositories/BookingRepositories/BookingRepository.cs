using BusinessObjects.Models;
using DAO;
using Repository.Repositories.AirporRepositories;
using Service.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repositories.BookingRepositories
{
    public class BookingRepository : GenericDAO<BookingInformation>, IBookingRepository
    {
        public async Task<List<BookingInformation>> GetAllBooking()
        {
            var list = await Get(orderBy: b => b.OrderByDescending(b => b.CreatedDate), includeProperties: "Tickets.TicketClass.SeatClass,Transactions");
            return list.ToList();
        }

        public async Task<List<BookingInformation>> GetAllBookingOfUser(string userId)
        {
            var list = await Get(b => b.UserId.Equals(userId),
    orderBy: b => b.OrderByDescending(b => b.CreatedDate), includeProperties: "Tickets.TicketClass.SeatClass,Transactions,Tickets.TicketClass.Flight");
            return list.ToList();
        }

        public async Task<BookingInformation> GetById(string id)
        {
            return await GetSingle(a => a.Id.Equals(id), includeProperties: "Tickets.TicketClass.SeatClass,Transactions");
        }

        public async Task<decimal> GetTotalPriceOfBooking(string id)
        {
            var booking = await GetSingle(b => b.Id.Equals(id), includeProperties: "Tickets.TicketClass");
            var classPrice = booking.Tickets.Select(t => t.TicketClass.Price).FirstOrDefault();
            return classPrice * booking.Quantity;
        }

        public async Task<List<BookingInformation>> GetAllPendingBookings()
        {
            var list = await Get(b => b.Status.Equals(BookingStatusEnums.Pending.ToString())
                                    , includeProperties: "Tickets.TicketClass.SeatClass,Transactions");
            return list.ToList();
        }
    }
}
