using BusinessObjects.Models;
using DAO;
using Repository.Repositories.SeatClassRepositories;
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
            return await GetSingle(s => s.Id.Equals(id));
        }

        public async Task<List<TicketClass>> GetAll()
        {
            var list = await Get();
            return list.ToList();
        }

    }
}