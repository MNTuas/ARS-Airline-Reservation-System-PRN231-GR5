using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service.Services.TransactionServices;
using Service.Services.VNPayServices;

namespace AirlinesReservationSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionController : ControllerBase
    {
        private readonly ITransactionService _transactionService;
        private readonly IVnPayService _vnPayService;

        public TransactionController(ITransactionService transactionService, IVnPayService vnPayService)
        {
            _transactionService = transactionService;
            _vnPayService = vnPayService;
            this._transactionService = transactionService;
        }

        [HttpPost]
        [Authorize(Roles = "User")]
        public async Task<IActionResult> CreateTransactionForBooking([FromBody] string bookingId)
        {
            string token = Request.Headers["Authorization"].ToString().Split(" ")[1];
            var result = await _transactionService.CreateTransaction(bookingId, token, HttpContext);
            return Ok(result);
        }
        [HttpGet]
        [Route("transaction-of-user/{userId}")]
        [Authorize(Roles ="Admin")]
        public async Task<IActionResult> GetTranSactionOfUser(string userId)
        {
            var trans = await _transactionService.GetTransactionByUserId(userId);
            return Ok(trans);
        }

        [HttpPut]
        [Route("{id}")]
        [Authorize(Roles = "User")]
        public async Task<IActionResult> UpdateTransactionStatus(string id, [FromBody] string status)
        {
            await _transactionService.UpdateTransactionStatus(id, status);
            return Ok("Update successfully!");
          
        }

        [HttpGet]
        [Route("payment-response")]
        [Authorize(Roles = "User")]
        public async Task<IActionResult> GetPaymentResponse([FromQuery] IQueryCollection keyValuePairs)
        {
            var response = _vnPayService.PaymentResponse(keyValuePairs);
            return Ok(response);
        }
    }
}
