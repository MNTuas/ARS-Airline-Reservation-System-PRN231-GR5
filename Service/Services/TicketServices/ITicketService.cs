using BusinessObjects.Models;
using BusinessObjects.RequestModels.Booking;
using BusinessObjects.RequestModels.Passenger;
using BusinessObjects.RequestModels.Ticket;
using FFilms.Application.Shared.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services.TicketServices
{
    public interface ITicketService
    {
        //    Task<Result<List<Ticket>>> AddTicket(CreateBookingRequest createBookingRequest,
        //List<CreatePassengerRequest> createPassengerRequests, CreateTicketRequest createTicketRequest);
        Task<Result<List<Ticket>>> addTicket(List<CreateTicketRequest> createTicketRequest);
    }
}
