using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Service.Services.TransactionService;

namespace AirlinesReservationSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionController : ControllerBase
    {
        private readonly ITransactionService _transactionService;

        public TransactionController(ITransactionService transactionService)
        {
            this._transactionService = transactionService;
        }
        [HttpGet]
        [Route("transaction-of-user/{userId}")]
        [Authorize(Roles ="Admin")]
        public async Task<IActionResult> GetTranSactionOfUser(string userId)
        {
            var tans = await _transactionService.GetTransactionByUserId(userId);
            return Ok(tans);
        }
        
    }
}
