using BusinessObjects.Models;
using Repository.Repositories.SeatClassRepositories;
using Repository.Repositories.TicketClassRepositories;
using Service.Services.SeatClassServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services.TicketClassServices
{
    public class TicketClassService : ITicketClassService
    {
        private readonly ITicketClassRepository _ticketClassRepository;

        public TicketClassService(ITicketClassRepository ticketClassRepository)
        {
            _ticketClassRepository = ticketClassRepository;
        }

        public async Task<List<TicketClass>> GetAllTicketClass()
        {
            return await _ticketClassRepository.GetAll();
        }
    }
}
