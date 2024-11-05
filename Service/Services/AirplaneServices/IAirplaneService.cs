using BusinessObjects.RequestModels.Airplane;
using BusinessObjects.ResponseModels.Airplane;

namespace Service.Services.AirplaneServices
{
    public interface IAirplaneService
    {
        Task<List<AirplaneResponseModel>> GetAllAirplane();
        Task AddAirplane(AddAirplaneRequest model);
        Task UpdateAirplaneAsync(string airplaneId, UpdateAirplaneRequest requestModel);
        Task<AirplaneResponseModel> GetAirplane(string id);
        Task SoftRemoveAirplane(string id);
    }
}
