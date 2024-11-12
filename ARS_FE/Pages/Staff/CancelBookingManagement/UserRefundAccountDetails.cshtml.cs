using BusinessObjects.ResponseModels.Booking;
using BusinessObjects.ResponseModels.RefundBankAccount;
using BusinessObjects.ResponseModels.Ticket;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using NuGet.Common;
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

        public List<TicketResponseModel> Tickets { get; set; } = default!;

        [BindProperty]
        public UserBookingResponseModel Booking { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(string id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var client = CreateAuthorizedClient();
            var response = await APIHelper.GetAsJsonAsync<RefundBankAccountResponseModel>(client, $"refund-bank-account/{id}");

            var responseBooking = await APIHelper.GetAsJsonAsync<UserBookingResponseModel>(client, $"Booking/{id}");

            if (responseBooking != null)
            {
                Tickets = responseBooking.Tickets;
                Booking = responseBooking;
            }

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

        public async Task<IActionResult> OnPostCancelAsync(string id)
        {
            if (id == null)
            {
                return BadRequest();
            }
            HttpContext.Session.SetString("BookingId", Booking.Id);
            var client = CreateAuthorizedClient();

            var returnUrlResponse = await APIHelper.PostAsJson(client, "refund-transaction", Booking.Id);
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
