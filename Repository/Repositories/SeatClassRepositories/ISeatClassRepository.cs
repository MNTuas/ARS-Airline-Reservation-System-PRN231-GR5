using BusinessObjects.Models;
using Repository.Repositories.GenericRepositories;

namespace Repository.Repositories.SeatClassRepositories
{
    public interface ISeatClassRepository : IGenericRepository<SeatClass>
    {
        Task<SeatClass> GetSeatClassBySeatClassName(string seatClassName);
        Task<List<SeatClass>> GetAllSeatClass();
    }
}
