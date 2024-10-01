using BusinessObjects.Models;
using BusinessObjects.ResponseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services.AIrlineServices
{
    public interface IAirlineService
    {
        Task<List<AllAirlinesResponseModel>> GetAllAirlines();
        Task<AirlinesResponseModel> GetDetailsAirlineInfo(string id);
        Task AddAirlines(string name);
        Task UpdateAirlines(string id, string name);
        Task ChangeAirlinesStatus(string id, string status);
    }
}
