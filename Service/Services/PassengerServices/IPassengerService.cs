using BusinessObjects.Models;
using BusinessObjects.RequestModels.Passenger;
using BusinessObjects.ResponseModels.Passenger;
using FFilms.Application.Shared.Response;

namespace Service.Services.PassengerServices
{
    public interface IPassengerService
    {
        Task<PassengerResposeModel> GetDetailsPassengerInfo(string id);
        Task<List<PassengerResposeModel>> GetAllPassengers();
        Task<Result<Passenger>> addPassenger(CreatePassengerRequest createPassengerRequest);
        Task UpdatePassenger(string id, UpdatePassengerRequest request);
        Task DeletePassenger(string id);
        Task<List<PassengerResposeModel>> GetPassengerByLogin();
    }
}
