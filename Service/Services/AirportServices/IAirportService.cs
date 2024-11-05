using BusinessObjects.Models;
using BusinessObjects.RequestModels.Airport;
using BusinessObjects.ResponseModels.Airport;
using FFilms.Application.Shared.Response;

namespace Service.Services.AirportService
{
    public interface IAirportService
    {
        Task<List<AirportResponseModel>> GetAllAirport();
        Task<Result<Airport>> AddAirport(CreateAirportRequest createAirportRequest);
        Task<Airport> GetDetailsAirportInfo(string id);
        Task UpdateAirports(string id, UpdateAirportRequest updateAirportRequest);
        Task ChangeAirportsStatus(string id);
    }
}
