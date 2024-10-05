using BusinessObjects.Models;
using BusinessObjects.RequestModels.Airplane;
using BusinessObjects.RequestModels.Rank;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services.AirplaneServices
{
    public interface IAirplaneService
    {
        Task<List<Airplane>> GetAllAirplane();

        Task<bool> AddAirplane(AddAirplaneRequest airplane);
        Task<bool> RemoveAirplane (Guid id);
        Task<bool> UpdateAirplane(string id, UpdateAirplaneRequest updateAirplane);
        Task<Airplane> GetAirlane(string id);
    }
}
