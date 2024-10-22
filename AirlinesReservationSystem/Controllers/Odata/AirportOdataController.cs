using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using Service.Services.AirportService;

namespace AirlinesReservationSystem.Controllers.Odata
{
    [Route("odata/airports")]
    [ApiController]
    public class AirportOdataController : ODataController
    {
        private readonly IAirportService _airportService;

        public AirportOdataController(IAirportService airportService)
        {
            _airportService = airportService;
        }

        [HttpGet]
        [EnableQuery]
        public async Task<IActionResult> GetAllAirport()
        {
            var result = await _airportService.GetAllAirport();
            return Ok(result);
        }
    }
}
