using BusinessObjects.Models;
using DAO;
using System;
using System.Collections.Generic;
using System.Linq;
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
    }
}
