using BusinessObjects.Models;
using DAO;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repositories.FlightRepositories
{
    public class FlightRepository : GenericDAO<Flight>, IFlightRepository
    {
        public async Task<List<Flight>> GetAllFlights()
        {
            var list = await Get();
            return list.ToList();
        }
    }
}
