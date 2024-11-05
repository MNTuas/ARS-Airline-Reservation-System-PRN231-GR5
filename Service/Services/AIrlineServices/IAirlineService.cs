using BusinessObjects.RequestModels.Airlines;
using BusinessObjects.ResponseModels.Airlines;

namespace Service.Services.AirlineServices
{
    public interface IAirlineService
    {
        Task<List<AllAirlinesResponseModel>> GetAllAirlines();
        Task<AirlinesResponseModel> GetDetailsAirlineInfo(string id);
        Task AddAirlines(AirlinesCreateModel model);
        Task UpdateAirlines(string id, AirlinesUpdateModel model);
        Task ChangeAirlinesStatus(string id);
    }
}
