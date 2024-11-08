using BusinessObjects.ResponseModels.Transaction;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Net.Http.Headers;

namespace ARS_FE.Pages.Admin.UserManagement
{
    public class TransactionsModel : PageModel
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public TransactionsModel(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public List<TransactionResponseModel> Transactions { get; set; } = new List<TransactionResponseModel>();

        [TempData]
        public string StatusMessage { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                StatusMessage = "User ID is required.";
                return Page();
            }

            var client = CreateAuthorizedClient();
            var response = await APIHelper.GetAsJsonAsync<List<TransactionResponseModel>>(client, $"transaction/transaction-of-user/{id}");

            if (response == null)
            {
                StatusMessage = "Error retrieving transactions.";
                return BadRequest();
            }
            else
            {
                Transactions = response;
                return Page();

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
