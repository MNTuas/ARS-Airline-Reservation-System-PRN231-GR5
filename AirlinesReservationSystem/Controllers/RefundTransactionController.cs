using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service.Services.RefundTransactionServices;

namespace AirlinesReservationSystem.Controllers
{
    [Route("api/refund-transaction")]
    [ApiController]
    public class RefundTransactionController : ControllerBase
    {
        private readonly IRefundTransactionService _refundTransactionService;

        public RefundTransactionController(IRefundTransactionService refundTransactionService)
        {
            _refundTransactionService = refundTransactionService;
        }

        [HttpPost]
        [Authorize(Roles = "Staff")]
        public async Task<IActionResult> CreateTransactionForBooking([FromBody] string bookingId)
        {
            string token = Request.Headers["Authorization"].ToString().Split(" ")[1];
            var result = await _refundTransactionService.RefundBookingTransaction(bookingId, token, HttpContext);
            return Ok(result);
        }

        [HttpPut]
        [Route("{id}")]
        [Authorize(Roles = "Staff")]
        public async Task<IActionResult> UpdateTransactionStatus(string id, [FromBody] string status)
        {
            await _refundTransactionService.UpdateRefundTransactionStatus(id, status);
            return Ok("Update successfully!");
        }
    }
}
