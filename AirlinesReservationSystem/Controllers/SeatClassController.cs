using Microsoft.AspNetCore.Mvc;
using Service.Services.SeatClassServices;

namespace AirlinesReservationSystem.Controllers
{
    [Route("api/seat-class")]
    [ApiController]
    public class SeatClassController : ControllerBase
    {
        private readonly ISeatClassService _seatClassService;

        public SeatClassController(ISeatClassService seatClassService)
        {
            _seatClassService = seatClassService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllSeatClass()
        {
            var result = await _seatClassService.GetAllSeatClass();
            return Ok(result);
        }
    }
}
