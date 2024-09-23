using BusinessObjects.Models;
using DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repositories.AirlineRepositories
{
    public class AirlineRepository : GenericDAO<Airline>, IAirlineRepository
    {
        public async Task<List<Airline>> GetAllAirlines()
        {
            var list = await Get();
            return list.ToList();
        }
    }
}
