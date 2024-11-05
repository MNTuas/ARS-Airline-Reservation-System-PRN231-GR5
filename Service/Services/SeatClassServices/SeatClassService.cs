using BusinessObjects.Models;
using Repository.Repositories.SeatClassRepositories;

namespace Service.Services.SeatClassServices
{
    public class SeatClassService : ISeatClassService
    {
        private readonly ISeatClassRepository _seatClassRepository;

        public SeatClassService(ISeatClassRepository seatClassRepository)
        {
            _seatClassRepository = seatClassRepository;
        }

        public async Task<List<SeatClass>> GetAllSeatClass()
        {
            return await _seatClassRepository.GetAllSeatClass();
        }


    }
}
