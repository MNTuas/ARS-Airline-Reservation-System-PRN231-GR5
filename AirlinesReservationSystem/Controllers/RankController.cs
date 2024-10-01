using BusinessObjects.RequestModels.Rank;
using Microsoft.AspNetCore.Mvc;
using Service.Services.RankServices;

namespace AirlinesReservationSystem.Controllers
{
    [Route("api/rank")]
    [ApiController]
    public class RankController : ControllerBase
    {
        private readonly IRankService _rankService;
        public RankController(IRankService rankService)
        {
            _rankService = rankService;
        }
        [HttpGet]
        [Route("all-rank")]
        public async Task<IActionResult> GetAllRank()
        {
            var listRank = await _rankService.GetAllRank();
            return Ok(listRank);
        }
        [HttpPost]
        [Route("add-rank")]
        public async Task<IActionResult> AddRank([FromBody]AddRankRequest addRankRequest)
        {
            var result = await _rankService.AddRank(addRankRequest);
            if (result)
            {
                return Ok(result);
            }
            return BadRequest();
        }
    }
}
