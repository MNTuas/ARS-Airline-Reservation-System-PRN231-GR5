using BusinessObjects.Models;
using Repository.Repositories.GenericRepositories;

namespace Repository.Repositories.TicketRepositories
{
    public interface ITicketRepository : IGenericRepository<Ticket>
    {
        Task<List<Ticket>> GetAllTicket();
        Task<Ticket> GetById(string id);
        Task<List<Ticket>> GetTicketByBookingId(string bookingId);
    }
}
