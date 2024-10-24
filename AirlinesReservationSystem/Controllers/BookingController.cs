using BusinessObjects.RequestModels.Booking;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service.Services.BookingServices;

namespace AirlinesReservationSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookingController : ControllerBase
    {
        private readonly IBookingService _bookingService;

        public BookingController(IBookingService bookingService)
        {
            _bookingService = bookingService;
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> CreateBooking(CreateBookingRequest createBookingRequest)
        {

            var result = await _bookingService.addBooking(createBookingRequest);

            if (result.Success)
            {
                return Ok(new
                {
                    message = result.Message,
                    bookingId = result.Data.Id, // Giả sử Data chứa BookingId
                    tickets = result.Data.Tickets // Danh sách vé
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
    }
}
