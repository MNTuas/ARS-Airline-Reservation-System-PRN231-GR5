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
            return Ok(listRank.ToList());
        }
        [HttpGet]
        [Route("get-rank/{id}")]
        public async Task<IActionResult> GetRank(string id)
        {
            var rank = await _rankService.GetRank(id);
            if (rank == null) {
                return BadRequest();
            }
            return Ok(rank);
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
        [HttpPut]
        [Route("update-rank/{id}")]
        public async Task<IActionResult> UpdateRank(string id,[FromBody] UpdateRankRequest updateRankRequest)
        {
            var result = await _rankService.UpdateRank(id, updateRankRequest);
            if (result)
            {
                return Ok(result);
            }
            return BadRequest();
        }
        
    }
}
