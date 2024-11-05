using BusinessObjects.Models;
using BusinessObjects.RequestModels.Ticket;
using FFilms.Application.Shared.Response;

namespace Service.Services.TicketServices
{
    public interface ITicketService
    {
        //    Task<Result<List<Ticket>>> AddTicket(CreateBookingRequest createBookingRequest,
        //List<CreatePassengerRequest> createPassengerRequests, CreateTicketRequest createTicketRequest);
        Task<Result<Ticket>> addTicket(CreateTicketRequest createTicketRequest);
    }
}
