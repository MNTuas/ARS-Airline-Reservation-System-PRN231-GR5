using BusinessObjects.Models;
using DAO;
using Repository.Repositories.BookingRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repositories.PassengerRepositories
{
    public class PassengerRepository : GenericDAO<Passenger>, IPassengerRepository
    {
        public async Task<List<Passenger>> GetAllPassenger()
        {
            var list = await Get();
            return list.ToList();
        }

        public async Task<Passenger> GetById(string id)
        {
            return await GetSingle(a => a.Id.Equals(id));
        }


    }
}
