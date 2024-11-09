using BusinessObjects.ResponseModels.Booking;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Service;
using System.Net.Http.Headers;

namespace ARS_FE.Pages.UserPage.BookingManager
{
    public class BookingListModel : PageModel
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public BookingListModel(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public PaginatedList<UserBookingResponseModel> Bookings { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? pageIndex)
        {
            var client = CreateAuthorizedClient();
            var response = await APIHelper.GetAsJsonAsync<List<UserBookingResponseModel>>(client, "Booking/own");
            if (response != null)
            {
                Bookings = PaginatedList<UserBookingResponseModel>.Create(response, pageIndex ?? 1, 10);
                return Page();
            }
            else
            {
                return RedirectToPage("/403Page");
            }
        }

        public async Task<IActionResult> OnPostCancelAsync(string id)
        {
            return RedirectToPage("./CreateRefundBankAccount", new { bookingId = id });
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
