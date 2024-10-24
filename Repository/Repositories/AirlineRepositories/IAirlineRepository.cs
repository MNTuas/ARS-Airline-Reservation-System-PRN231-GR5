using BusinessObjects.Models;
using BusinessObjects.ResponseModels;
using BusinessObjects.ResponseModels.Airlines;
using Repository.Repositories.GenericRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repositories.AirlineRepositories
{
    public interface IAirlineRepository : IGenericRepository<Airline>
    {
        Task<List<Airline>> GetAllAirlines();
        Task<Airline> GetById(string id);
        Task<Airline> GetDetailsById(string id);
    }
}
