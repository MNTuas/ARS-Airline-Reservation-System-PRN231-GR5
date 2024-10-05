using BusinessObjects.Models;
using DAO;
using Repository.Repositories.RankRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repositories.FlightClassRepositories
{
    public class FlightClassRepository : GenericDAO<FlightClass>, IFlightClassRepository
    {
        public async Task<FlightClass> GetById(string id)
        {
            return await GetSingle(a => a.Id.Equals(id));
        }

        public async Task<List<FlightClass>> GetAllFlightClasses()
        {
            var list = await Get();
            return list.ToList();
        }
    }
}
