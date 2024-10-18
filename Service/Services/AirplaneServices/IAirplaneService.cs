using BusinessObjects.Models;
using BusinessObjects.RequestModels.Airplane;
using BusinessObjects.RequestModels.Rank;
using BusinessObjects.ResponseModels.Airplane;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
