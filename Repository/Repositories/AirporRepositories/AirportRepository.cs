using BusinessObjects.Models;
using DAO;

namespace Repository.Repositories.AirporRepositories
{
    public class AirportRepository : GenericDAO<Airport>, IAirportRepository
    {
        public async Task<List<Airport>> GetAllAirport()
        {
            var list = await Get();
            return list.ToList();
        }

        public async Task<Airport> GetById(string id)
        {
            return await GetSingle(a => a.Id.Equals(id));
        }

        public async Task<Airport> GetAirportByCodeAsync(string airportName)
        {
            var airplane = await GetSingle(r => r.Name.Equals(airportName));
            return airplane;
        }

    }
}
