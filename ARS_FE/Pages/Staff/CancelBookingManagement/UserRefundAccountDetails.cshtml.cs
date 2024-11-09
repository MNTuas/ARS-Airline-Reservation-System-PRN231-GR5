using BusinessObjects.ResponseModels.RefundBankAccount;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Net.Http.Headers;

namespace ARS_FE.Pages.Staff.CancelBookingManagement
{
    public class UserRefundAccountDetailsModel : PageModel
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public UserRefundAccountDetailsModel(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public RefundBankAccountResponseModel RefundBankAccount { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(string id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var client = CreateAuthorizedClient();

            var response = await APIHelper.GetAsJsonAsync<RefundBankAccountResponseModel>(client, $"refund-bank-account/{id}");

            if (response != null)
            {
                RefundBankAccount = response;
                return Page();
            }
            else
            {
                return BadRequest();
            }

        }

        private HttpClient CreateAuthorizedClient()
        {
            var client = _httpClientFactory.CreateClient("ApiClient");
            var token = HttpContext.Session.GetString("JWToken");

            if (!string.IsNullOrEmpty(token))
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            }

            return client;
        }
    }
}
