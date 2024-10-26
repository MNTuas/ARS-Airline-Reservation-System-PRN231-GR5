using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BusinessObjects.Models;
using DAO;
using System.Net.Http.Headers;
using BusinessObjects.ResponseModels.Ticket;
using BusinessObjects.ResponseModels.Booking;

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

        public async Task<IActionResult> OnGetAsync(string id, int? pageIndex)
        {
            if (id == null)
            {
                return NotFound();
            }
            var client = CreateAuthorizedClient();


            var response = await APIHelper.GetAsJsonAsync<UserBookingResponseModel>(client, $"Booking/{id}");

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