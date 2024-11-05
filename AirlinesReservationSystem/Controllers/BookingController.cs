using BusinessObjects.RequestModels.Booking;
using Microsoft.AspNetCore.Authorization;
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
        [Authorize(Roles = "User")]
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

        [HttpPut]
        [Authorize(Roles = "User")]
        [Route("{id}")]
        public async Task<IActionResult> UpdateBookingStatus(string id, [FromBody] string status)
        {
            await _bookingService.UpdateBookingStatus(id, status);
            return Ok("Update successfully!");
        }

        [HttpGet]
        [Authorize(Roles = "User")]
        [Route("own")]
        public async Task<IActionResult> GetOwnBookings()
        {
            var response = await _bookingService.GetOwnBookings();
            return Ok(response);
        }

        [HttpGet]
        [Authorize(Roles = "User,Admin")]
        [Route("{id}")]
        public async Task<IActionResult> GetBookingById(string id)
        {
            var response = await _bookingService.GetBookingById(id);
            return Ok(response);
        }

        [HttpPut]
        [Authorize(Roles = "User")]
        [Route("cancel")]
        public async Task<IActionResult> CancelBooking([FromBody] string id)
        {
            await _bookingService.CancelBooking(id);
            return Ok("Cancel booking successfully!");
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetAllBookings()
        {
            var response = await _bookingService.GetAllBookings();
            return Ok(response);
        }
    }
}
