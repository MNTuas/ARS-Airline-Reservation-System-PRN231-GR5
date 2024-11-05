using BusinessObjects.Models;
using Repository.Repositories.GenericRepositories;

namespace Repository.Repositories.AirplaneRepositories
{
    public interface IAirplaneRepository : IGenericRepository<Airplane>
    {
        Task<List<Airplane>> GetAllAirplaneAsync();
        Task<List<Airplane>> GetAllActiveAirplanes();
        Task<Airplane> GetAirplane(string id);
        Task<Airplane> GetAirplaneByCodeAsync(string airplaneCode);
    }
}
