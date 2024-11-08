using BusinessObjects.Models;
using Repository.Repositories.GenericRepositories;

namespace Repository.Repositories.PassengerRepositories
{
    public interface IPassengerRepository : IGenericRepository<Passenger>
    {
        Task<List<Passenger>> GetAllPassenger();
        Task<Passenger> GetById(string id);
        Task<List<Passenger>> GetByLogin(string id);
    }
}
