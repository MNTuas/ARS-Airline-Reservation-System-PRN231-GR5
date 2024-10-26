using BusinessObjects.Models;
using BusinessObjects.ResponseModels.Airport;
using BusinessObjects.ResponseModels.Booking;
using BusinessObjects.ResponseModels.Flight;
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

        //[BindProperty(SupportsGet = true)]
        //public DateTime? FromDate { get; set; }

        public async Task<IActionResult> OnGetAsync(int? pageIndex)
        {
            var client = CreateAuthorizedClient();
            var response = await APIHelper.GetAsJsonAsync<List<UserBookingResponseModel>>(client, "Booking/own");
            if (response != null)
            {
                Bookings = PaginatedList<UserBookingResponseModel>.Create(response, pageIndex ?? 1, 6);
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
