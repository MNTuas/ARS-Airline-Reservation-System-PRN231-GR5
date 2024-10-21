using BusinessObjects.Models;
using BusinessObjects.RequestModels.Flight;
using BusinessObjects.ResponseModels.Flight;

namespace Service.Services.FlightServices
{
    public interface IFlightService
    {
        Task<List<FlightResponseModel>> GetAllFlights();
        Task<FlightResponseModel> GetFlightById(string id);
        Task CreateFlight(CreateFlightRequest request);
        Task UpdateFlight(string flightId, UpdateFlightRequest request);
    }
}
