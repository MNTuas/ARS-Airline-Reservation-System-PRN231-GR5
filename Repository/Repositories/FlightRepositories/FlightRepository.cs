using BusinessObjects.Models;
using BusinessObjects.ResponseModels.Flight;
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
            var list = await Get(includeProperties: "FromNavigation,ToNavigation,Airplane.Airlines");
            return list.ToList();
        }

        public async Task<Flight> GetFlightById(string id)
        {
            return await GetSingle(f => f.Id.Equals(id), includeProperties: "FromNavigation,ToNavigation,Airplane.Airlines,TicketClasses.SeatClass");
        }
    }
}
