using BusinessObjects.Models;
using Repository.Repositories.GenericRepositories;

namespace Repository.Repositories.FlightRepositories
{
    public interface IFlightRepository : IGenericRepository<Flight>
    {
        Task<List<Flight>> GetAllFlights();
        Task<Flight> GetFlightById(string id);
        Task<List<Flight>> GetFlightsByFilter(string from, string to, DateTime checkin, DateTime? checkout);
        Task<Flight> GetFlightByNumber(string flightNumber, DateTime departureTime);
        Task<List<Flight>> GetAllScheduledFlight();
        Task<int> CountFlightsForAirplaneOnDate(string airplaneId, DateTime departureTime);
    }
}
