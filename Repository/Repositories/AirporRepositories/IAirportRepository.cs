using BusinessObjects.Models;
using Repository.Repositories.GenericRepositories;

namespace Repository.Repositories.AirporRepositories
{
    public interface IAirportRepository : IGenericRepository<Airport>
    {
        Task<List<Airport>> GetAllAirport();
        Task<Airport> GetById(string id);
        Task<Airport> GetAirportByCodeAsync(string airportName);
    }
}
