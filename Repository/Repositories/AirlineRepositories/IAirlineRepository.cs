using BusinessObjects.Models;
using Repository.Repositories.GenericRepositories;

namespace Repository.Repositories.AirlineRepositories
{
    public interface IAirlineRepository : IGenericRepository<Airline>
    {
        Task<List<Airline>> GetAllAirlines();
        Task<Airline> GetById(string id);
        Task<Airline> GetDetailsById(string id);
    }
}
