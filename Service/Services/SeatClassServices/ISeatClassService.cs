using BusinessObjects.Models;

namespace Service.Services.SeatClassServices
{
    public interface ISeatClassService
    {
        Task<List<SeatClass>> GetAllSeatClass();
    }
}
