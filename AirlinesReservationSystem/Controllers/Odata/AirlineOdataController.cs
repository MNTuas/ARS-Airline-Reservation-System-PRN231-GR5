using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using Service.Services.AirlineServices;

namespace AirlinesReservationSystem.Controllers.Odata
{
    [Route("odata/airlines")]
    [ApiController]
    public class AirlineOdataController : ODataController
    {
        private readonly IAirlineService _AirlineService;

        public AirlineOdataController(IAirlineService AirlineService)
        {
            _AirlineService = AirlineService;
        }

        [HttpGet]
        [EnableQuery]
        public async Task<IActionResult> GetAllAirlinesDetails()
        {
            var result = await _AirlineService.GetAllAirlines();
            return Ok(result);
        }
    }
}
