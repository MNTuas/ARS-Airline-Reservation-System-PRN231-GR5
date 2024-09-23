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
        Task AddAirlines(string name);
    }
}
