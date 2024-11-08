using BusinessObjects.Models;
using DAO;

namespace Repository.Repositories.SeatClassRepositories
{
    public class SeatClassRepository : GenericDAO<SeatClass>, ISeatClassRepository
    {
        public async Task<SeatClass> GetSeatClassBySeatClassName(string seatClassName)
        {
            return await GetSingle(s => s.Name.Equals(seatClassName));
        }

        public async Task<List<SeatClass>> GetAllSeatClass()
        {
            var list = await Get();
            return list.ToList();
        }

    }
}
