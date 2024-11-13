using BusinessObjects.ResponseModels.Booking;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Service;
using System.Net.Http.Headers;

namespace ARS_FE.Pages.Staff.CancelBookingManagement
{
    public class IndexModel : PageModel
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public IndexModel(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        [BindProperty(SupportsGet = true)]
        public string? Username { get; set; }

        public PaginatedList<BookingResponseModel> Bookings { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? pageIndex)
        {
            var client = CreateAuthorizedClient();
            var response = await APIHelper.GetAsJsonAsync<List<BookingResponseModel>>(client, "Booking/refund");

            if (response != null)
            {
                if (!string.IsNullOrEmpty(Username))
                {
                    response = response.Where(b => b.UserName.Contains(Username, StringComparison.OrdinalIgnoreCase)).ToList();
                }

                Bookings = PaginatedList<BookingResponseModel>.Create(response, pageIndex ?? 1, 10);
                return Page();
            }
            else
            {
                return RedirectToPage("/403Page");
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
