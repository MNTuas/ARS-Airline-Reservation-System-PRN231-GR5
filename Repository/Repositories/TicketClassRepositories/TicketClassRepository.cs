using BusinessObjects.Models;
using DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repositories.TicketClassRepositories
{
    public class TicketClassRepository : GenericDAO<TicketClass>, ITicketClassRepository
    {
        public async Task<TicketClass> GetTicketClassById(string id)
        {
            return await GetSingle(t => t.Id.Equals(id));
        }
    }
}
