using BusinessObjects.Models;
using BusinessObjects.ResponseModels;
using DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repositories.AirporRepositories
{
    public class AirportRepository : GenericDAO<Airport>, IAirportRepository
    {
        public async Task<List<Airport>> GetAllAirport()
        {
            var list = await Get();
            return list.ToList();
        }

        public async Task<Airport> GetById(string id)
        {
            return await GetSingle(a => a.Id.Equals(id));
        }

        public async Task<Airport> GetAirportByCodeAsync(string airportName)
        {
            var airplane = await GetSingle(r => r.Name.Equals(airportName));
            return airplane;
        }

    }
}
