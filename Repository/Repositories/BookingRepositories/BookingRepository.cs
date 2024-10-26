using BusinessObjects.Models;
using DAO;
using Repository.Repositories.AirporRepositories;
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
            var list = await Get();
            return list.ToList();
        }

        public async Task<BookingInformation> GetById(string id)
        {
            return await GetSingle(a => a.Id.Equals(id));
        }

    }
}
