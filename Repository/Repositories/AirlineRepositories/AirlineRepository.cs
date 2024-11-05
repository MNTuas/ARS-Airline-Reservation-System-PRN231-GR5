using BusinessObjects.Models;
using DAO;

namespace Repository.Repositories.AirlineRepositories
{
    public class AirlineRepository : GenericDAO<Airline>, IAirlineRepository
    {
        public async Task<List<Airline>> GetAllAirlines()
        {
            var list = await Get();
            return list.ToList();
        }

        public async Task<Airline> GetById(string id)
        {
            return await GetSingle(a => a.Id.Equals(id));
        }
        public async Task<Airline> GetDetailsById(string id)
        {
            return await GetSingle(a => a.Id.Equals(id), includeProperties: "Airplanes.AirplaneSeats.SeatClass");
        }
    }
}
