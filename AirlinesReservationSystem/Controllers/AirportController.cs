using BusinessObjects.Models;
using BusinessObjects.RequestModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service.Services.AirportService;

namespace AirlinesReservationSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AirportController : ControllerBase
    {
        private readonly IAirportService _airportService;

        public AirportController(IAirportService airportService) 
        {
            _airportService = airportService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAirlines()
        {
            var response = await _airportService.GetAllAirport();
            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> AddNewAirlines(CreateAirportRequest createAirportRequest)
        {
           var results = await _airportService.AddAirport(createAirportRequest);
           if(results.Success != false)
           {
                return Ok(new
                {
                    Status = results.Success,
                    Message = results.Message,
                    Data = results.Data 
                });
           }
            return BadRequest(new
            {
                Status = results.Success,
                Message = results.Message
            });
        }
    }
}
