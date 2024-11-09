using BusinessObjects.RequestModels.RefundBankAccount;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service.Services.RefundBankAccountServices;

namespace AirlinesReservationSystem.Controllers
{
    [Route("api/refund-bank-account")]
    [ApiController]
    public class RefundBankAccountController : ControllerBase
    {
        private readonly IRefundBankAccountService _refundBankAccountService;

        public RefundBankAccountController(IRefundBankAccountService refundBankAccountService)
        {
            _refundBankAccountService = refundBankAccountService;
        }

        [HttpGet]
        [Authorize(Roles = "User,Staff")]
        [Route("{bookingId}")]
        public async Task<IActionResult> GetBankingAccountByBookingId(string bookingId)
        {
            var response = await _refundBankAccountService.GetRefundBankingAccountByBookingId(bookingId);
            return Ok(response);
        }

        [HttpPost]
        [Authorize(Roles = "User")]
        public async Task<IActionResult> CreateRefundBankingAccount(RefundBankAccountCreateModel model)
        {
            await _refundBankAccountService.CreateRefundBankAccount(model);
            return Ok("Create banking account successfully!");
        }

        [HttpPut]
        [Route("{bookingId}")]
        [Authorize(Roles = "User")]
        public async Task<IActionResult> UpdateRefundBankingAccount(RefundBankAccountUpdateModel model, string bookingId)
        {
            await _refundBankAccountService.UpdateRefundBankAccount(model, bookingId);
            return Ok("Update banking account successfully!");
        }
    }
}
