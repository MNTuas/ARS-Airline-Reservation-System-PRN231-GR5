using BusinessObjects.Models;
using DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repositories.SeatClassRepositories
{
    public class SeatClassRepository : GenericDAO<SeatClass>, ISeatClassRepository
    {
        public async Task<SeatClass> GetSeatClassBySeatClassName(string seatClassName)
        {
            return await GetSingle(s => s.Name.Equals(seatClassName));
        }

        public async Task<List<SeatClass>> GetAllSeatClass()
        {
            var list = await Get();
            return list.ToList();
        }

    }
}
