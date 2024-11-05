using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BusinessObjects.Models;
using DAO;
using BusinessObjects.ResponseModels.Flight;
using System.Net.Http.Headers;
using BusinessObjects.RequestModels.Airport;
using BusinessObjects.RequestModels.Booking;
using System.Text.Json;

namespace ARS_FE.Pages.UserPage.BookingManager
{
    public class IndexModel : PageModel
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public IndexModel(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public FlightResponseModel Flight { get; set; } = default!;

        [BindProperty]
        public BookingInformation BookingInformation { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(string id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var client = CreateAuthorizedClient();
            if (client == null)
            {
                // Lấy các thông tin từ query string
                var from = HttpContext.Request.Query["from"];
                var to = HttpContext.Request.Query["to"];
                var checkin = HttpContext.Request.Query["checkin"];
                var checkout = HttpContext.Request.Query["checkout"];

                // Tạo URL quay lại với các thông tin đã lấy từ query string
                var returnUrl = $"https://localhost:7223/SearchFlight?from={from}&to={to}&checkin={checkin}&checkout={checkout}";

                // Chuyển hướng đến trang Login và gửi returnUrl
                return RedirectToPage("/Login", new { ReturnUrl = returnUrl });

            }

            var response = await APIHelper.GetAsJsonAsync<FlightResponseModel>(client, $"Flight/{id}");

            if (response != null)
            {
                Flight = response;
                return Page();
            }
            else
            {
                return BadRequest();
            }
        }

        [BindProperty]
        public CreateBookingRequest createBookingRequest { get; set; } = default!;

        [BindProperty]
        public string SelectedTicketClass { get; set; }

        [BindProperty]
        public string flightId { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            var client = CreateAuthorizedClient();
            if (client == null)
            {
                return RedirectToPage("/Login");
            }
            // Lấy SeatClassId đã chọn từ form
            string seatClassId = SelectedTicketClass;
           

            return RedirectToPage("/UserPage/TicketManagement/Index", new
            { quantity = createBookingRequest.Quantity,
                ticketClassId = seatClassId,
                flightId = flightId,
            });
        }

        private HttpClient? CreateAuthorizedClient()
        {
            var client = _httpClientFactory.CreateClient("ApiClient");
            var token = HttpContext.Session.GetString("JWToken");
           
            if (!string.IsNullOrEmpty(token))
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                return client;
            }

            return null;
        }

    }
}