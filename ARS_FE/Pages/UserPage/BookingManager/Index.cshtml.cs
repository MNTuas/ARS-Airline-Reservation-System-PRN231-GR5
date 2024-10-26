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
                return RedirectToPage("/Login");
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

        public async Task<IActionResult> OnPostAsync()
        {
            //if (!ModelState.IsValid)
            //{
            //    return Page();
            //}

            var client = CreateAuthorizedClient();
            if (client == null)
            {
                return RedirectToPage("/Login");
            }

            // Lấy SeatClassId đã chọn từ form
            string seatClassId = SelectedTicketClass;

            var n = new CreateBookingRequest
            {
                Quantity = createBookingRequest.Quantity,
            };

            var response = await APIHelper.PostAsJson(client, "Booking", n);

            if (response.IsSuccessStatusCode)
            {
                // Đọc nội dung phản hồi JSON bên api
                var responseContent = await response.Content.ReadAsStringAsync();
                var createBookingResponse = JsonDocument.Parse(responseContent);

                // Lấy BookingId từ phản hồi
                string bookingId = createBookingResponse.RootElement.GetProperty("bookingId").GetString();

                
                // Nếu Booking tạo thành công, chuyển hướng đến trang Ticket
                return RedirectToPage("/UserPage/TicketManagement/Index", new 
                { quantity = createBookingRequest.Quantity, 
                  ticketClassId = seatClassId,
                  bookingId = bookingId

                });
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Error occurred while creating the Booking.");
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
                return client;
            }

            return null;
        }

    }
}