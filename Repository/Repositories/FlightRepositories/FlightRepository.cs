using BusinessObjects.Models;
using DAO;
using Repository.Enums;
using System.Linq.Expressions;

namespace Repository.Repositories.FlightRepositories
{
    public class FlightRepository : GenericDAO<Flight>, IFlightRepository
    {

        public async Task<List<Flight>> GetAllFlights(string? flightNumber = null)
        {
            Expression<Func<Flight, bool>> filter = f => true;

            if (!string.IsNullOrEmpty(flightNumber))
            {
                filter = f => f.FlightNumber.Contains(flightNumber);
            }

            var list = await Get(filter, orderBy: f => f.OrderBy(f => f.DepartureTime), includeProperties: "FromNavigation,ToNavigation,Airplane.Airlines,TicketClasses");
            return list.ToList();
        }

        public async Task<Flight> GetFlightById(string id)
        {
            return await GetSingle(f => f.Id.Equals(id), includeProperties: "FromNavigation,ToNavigation,Airplane.Airlines,TicketClasses.SeatClass,TicketClasses.Tickets,Airplane.AirplaneSeats");
        }

        public async Task<List<Flight>> GetFlightsByFilter(string from, string to, DateTime checkin, DateTime? checkout)
        {
            var allFlights = await GetAllFlights();
            allFlights = allFlights.Where(f => f.TicketClasses.Any(t => t.RemainSeat > 0)).ToList();

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

        public async Task<int> CountFlightsForAirplaneOnDate(string airplaneId, DateTime departureTime)
        {
            // Truy vấn số chuyến bay cho máy bay với ngày khởi hành trong ngày đó
            var flight = await Get(f => f.AirplaneId == airplaneId && f.DepartureTime.Date == departureTime);
            return flight.Count();
        }


        public async Task<List<Flight>> GetAllScheduledFlight()
        {
            var list = await Get(f => f.Status.Equals(FlightStatusEnums.Scheduled.ToString()));
            return list.ToList();
        }
    }
}
