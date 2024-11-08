using BusinessObjects.ResponseModels.Booking;
using BusinessObjects.ResponseModels.Ticket;
using BusinessObjects.ResponseModels.User;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Net.Http.Headers;

namespace ARS_FE.Pages.UserPage.TicketManagement
{
    public class DetailsTransactionModel : PageModel
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public DetailsTransactionModel(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public List<TicketResponseModel> Tickets { get; set; } = default!;
        public decimal Discount { get; set; }
        public string BookingId { get; set; }

        public async Task<IActionResult> OnGetAsync(string bookingId)
        {
            if (bookingId == null)
            {
                return NotFound();
            }
            var client = CreateAuthorizedClient();
            BookingId = bookingId;

            var response = await APIHelper.GetAsJsonAsync<UserBookingResponseModel>(client, $"Booking/{bookingId}");
            var userInfo = await APIHelper.GetAsJsonAsync<UserInfoResponseModel>(client, "users/own");

            Discount = userInfo.Discount;

            if (response != null)
            {
                Tickets = response.Tickets;
                return Page();
            }
            else
            {
                return BadRequest();
            }
        }

        public async Task<IActionResult> OnPostCheckoutAsync(string bookingId)
        {
            var client = CreateAuthorizedClient();

            var returnUrlResponse = await APIHelper.PostAsJson(client, $"Transaction", bookingId);
            var returnUrl = await returnUrlResponse.Content.ReadFromJsonAsync<string>();
            return Redirect(returnUrl);
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
