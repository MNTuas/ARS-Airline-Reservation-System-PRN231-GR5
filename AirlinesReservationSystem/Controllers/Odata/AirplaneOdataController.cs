using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using Service.Services.AirplaneServices;

namespace AirlinesReservationSystem.Controllers.Odata
{
    [Route("odata/airplanes")]
    [ApiController]
    public class AirplaneOdataController : ODataController
    {
        private readonly IAirplaneService _airplaneService;

        public AirplaneOdataController(IAirplaneService airplaneService)
        {
            _airplaneService = airplaneService;
        }

        [HttpGet]
        [EnableQuery]
        public async Task<IActionResult> GetAllAirplane()
        {
            var result = await _airplaneService.GetAllAirplane();
            return Ok(result);
        }
    }
}
