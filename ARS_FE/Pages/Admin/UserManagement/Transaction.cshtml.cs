using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using BusinessObjects.ResponseModels.Transaction;

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
        public string StatusMessage { get; set; }

        public async Task<IActionResult> OnGetAsync(string userId)
        {
            Console.WriteLine("OnGetAsync called with userId: " + userId);
            if (string.IsNullOrEmpty(userId))
            {
                StatusMessage = "User ID is required.";
                return Page();
            }

            var client = CreateAuthorizedClient();
            var response = await APIHelper.GetAsJsonAsync<List<TransactionResponseModel>>(client, $"transaction/transaction-of-user/{userId}");

            if (response == null || response.Count == 0)
            {
                StatusMessage = "No transactions found for this user.";
            }
            else
            {
                Transactions = response;
            }

            return Page();
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
