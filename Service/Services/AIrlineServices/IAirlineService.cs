using BusinessObjects.Models;
using BusinessObjects.RequestModels.Airlines;
using BusinessObjects.ResponseModels.Airlines;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
