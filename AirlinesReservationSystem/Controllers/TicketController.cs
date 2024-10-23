using BusinessObjects.RequestModels.Booking;
using BusinessObjects.RequestModels.Passenger;
using BusinessObjects.RequestModels.Ticket;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service.Services.TicketServices;

namespace AirlinesReservationSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TicketController : ControllerBase
    {
        private readonly ITicketService _ticketService;

        public TicketController(ITicketService ticketService)
        {
            _ticketService = ticketService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateTicket(Request request)
        {
            var bookingRequest = request.CreateBookingRequest;
            var passengerRequests = request.CreatePassengerRequests; // Now a list of passengers
            var ticketRequest = request.CreateTicketRequest;

            var result = await _ticketService.AddTicket(bookingRequest, passengerRequests, ticketRequest);

            if (result.Success)
            {
                return Ok(new
                {
                    message = result.Message,
                    tickets = result.Data
                });
            }
            else
            {
                return BadRequest(new
                {
                    message = result.Message
                });
            }
        }

        // Define the request model
        public class Request
        {
            public CreateBookingRequest CreateBookingRequest { get; set; }
            public List<CreatePassengerRequest> CreatePassengerRequests { get; set; } // List of passengers
            public CreateTicketRequest CreateTicketRequest { get; set; }
        }
    }
    }
