using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using BusinessObjects.RequestModels;
using BusinessObjects.RequestModels.Auth;
using DAO;
using ARS_FE.Pages.UserPage.BookingManager; 

namespace ARS_FE.Pages.UserPage.Transactions
{
    public class TransactionsModel : PageModel
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public TransactionsModel(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public List<TransactionResponse> Transactions { get; set; } = new List<TransactionResponse>();

        [TempData]
        public string StatusMessage { get; set; }

        public async Task<IActionResult> OnGetAsync(string userId)
        {
            var client = CreateAuthorizedClient();

           
            Transactions = await APIHelper.GetAsJsonAsync<List<TransactionResponse>>(client, $"transaction-of-user/{userId}");

            if (Transactions == null)
            {
                StatusMessage = "An error occurred while retrieving transactions.";
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
