using BusinessObjects.Models;
using BusinessObjects.RequestModels.Flight;
using BusinessObjects.ResponseModels.Flight;
using FFilms.Application.Shared.Response;
using Microsoft.AspNetCore.Http;

namespace Service.Services.FlightServices
{
    public interface IFlightService
    {
        Task<List<FlightResponseModel>> GetAllFlights();
        Task<FlightResponseModel> GetFlightById(string id);
        Task CreateFlight(CreateFlightRequest request);
        Task UpdateFlight(string flightId, UpdateFlightRequest request);
        Task<List<FlightResponseModel>> GetFlightByFilter(string from, string to, DateTime checkin, DateTime? checkout);
        Task<Result<Flight>> UploadFile(IFormFile file);
    }
}
