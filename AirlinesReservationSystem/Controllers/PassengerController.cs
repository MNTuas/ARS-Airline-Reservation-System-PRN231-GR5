using BusinessObjects.RequestModels.Passenger;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service.Services.PassengerServices;

namespace AirlinesReservationSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PassengerController : ControllerBase
    {
        private readonly IPassengerService _PassengerService;

        public PassengerController(IPassengerService PassengerService)
        {
            _PassengerService = PassengerService;
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> CreatePassenger(CreatePassengerRequest createPassengerRequest)
        {

            var result = await _PassengerService.addPassenger(createPassengerRequest);

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
    }
}
