using BusinessObjects.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services.AIrlineServices
{
    public interface IAirlineService
    {
        Task<List<Airline>> GetAllAirlines();
        Task<Airline> GetAirlineInfo(string id);
        Task AddAirlines(string name);
        Task UpdateAirlines(string id, string name);
    }
}
