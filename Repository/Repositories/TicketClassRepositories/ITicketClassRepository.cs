using BusinessObjects.Models;
using Repository.Repositories.GenericRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repositories.TicketClassRepositories
{
    public interface ITicketClassRepository : IGenericRepository<TicketClass>
    {
        Task<TicketClass> GetTicketClassById(string id);

        Task<List<TicketClass>> GetAll();
    }
}
