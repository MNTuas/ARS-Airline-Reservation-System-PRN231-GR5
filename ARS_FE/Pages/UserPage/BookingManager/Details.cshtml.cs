using BusinessObjects.ResponseModels.Booking;
using BusinessObjects.ResponseModels.Ticket;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Net.Http.Headers;
using BusinessObjects.ResponseModels.User;
using BusinessObjects.ResponseModels.Flight;


namespace ARS_FE.Pages.UserPage.BookingManager
{
    public class DetailsModel : PageModel
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public DetailsModel(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public List<TicketResponseModel> Tickets { get; set; } = default!;
        public decimal Discount { get; set; }
        public FlightResponseModel Flight { get; set; } = default!;


        public async Task<IActionResult> OnGetAsync(string id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var client = CreateAuthorizedClient();

            var response = await APIHelper.GetAsJsonAsync<UserBookingResponseModel>(client, $"Booking/{id}");
            var userInfo = await APIHelper.GetAsJsonAsync<UserInfoResponseModel>(client, "users/own");

            Discount = userInfo.Discount.Value;
            var flight = await APIHelper.GetAsJsonAsync<FlightResponseModel>(client, $"Flight/{response.FlightId}");

            if (flight != null)
            {
                Flight = flight;
            }

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