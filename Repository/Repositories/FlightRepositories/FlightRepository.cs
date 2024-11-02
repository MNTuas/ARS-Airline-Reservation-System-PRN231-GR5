using BusinessObjects.Models;
using BusinessObjects.ResponseModels.Flight;
using DAO;
using Microsoft.EntityFrameworkCore;
using Repository.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
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
            return await GetSingle(f => f.Id.Equals(id), includeProperties: "FromNavigation,ToNavigation,Airplane.Airlines,TicketClasses.SeatClass,TicketClasses.Tickets,Airplane.AirplaneSeats");
        }

        public async Task<List<Flight>> GetFlightsByFilter(string from, string to, DateTime checkin, DateTime? checkout)
        {
            var allFlights = await GetAllFlights();

            var filteredFlights = allFlights.Where(f => f.From.Equals(from)
                                                      && f.To.Equals(to)
                                                      && f.DepartureTime.Date == checkin.Date);

            if (checkout.HasValue)
            {
                filteredFlights = filteredFlights.Where(f => f.ArrivalTime.Date == checkout.Value.Date);
            }

            return filteredFlights.ToList();
        }

        public async Task<Flight> GetFlightByNumber(string flightNumber, DateTime departureTime)
        {
            var flight = await GetSingle(r => r.FlightNumber.Equals(flightNumber) && r.DepartureTime.Date == departureTime.Date);
            return flight;
        }

        public async Task<List<Flight>> GetAllScheduledFlight()
        {
            var list = await Get(f => f.Status.Equals(FlightStatusEnums.Scheduled.ToString()));
            return list.ToList();
        }
    }
}
