using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BusinessObjects.Models;
using DAO;
using BusinessObjects.RequestModels;
using System.Net.Http.Headers;
using Service.Services.FlightClassServices;
using Newtonsoft.Json;

namespace ARS_FE.Pages.Staff.FlightClassManagement
{
    public class EditModel : PageModel
    {
        private readonly IHttpClientFactory _httpClientFactory;
        public EditModel(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        [BindProperty]
        public FlightClass FlightClass { get; set; } = default!;

        [BindProperty]
        public FlightClassRequest FlightClassRequest { get; set; } = new FlightClassRequest();

        public async Task<IActionResult> OnGetAsync(string id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var client = CreateAuthorizedClient();

            var response = await APIHelper.GetAsJsonAsync<FlightClass>(client, $"FlightClass/{id}");
            if (response != null)
            {
                FlightClass = response;
                FlightClassRequest = new FlightClassRequest
                {
                    FlightId = FlightClass.FlightId,
                    Class = FlightClass.Class,
                    Quantity = FlightClass.Quantity,
                    Price = FlightClass.Price
                };
                return Page();
            }
            else
            {
                return BadRequest();
            }
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList();
                foreach (var error in errors)
                {
                    Console.WriteLine(error);
                }
                return Page();
            }

            var client = CreateAuthorizedClient();

            FlightClass.FlightId = FlightClassRequest.FlightId;
            FlightClass.Class = FlightClassRequest.Class;
            FlightClass.Quantity = FlightClassRequest.Quantity;
            FlightClass.Price = FlightClassRequest.Price;

            var response = await APIHelper.PutAsJson(client, $"FlightClass/{FlightClass.Id}", FlightClass);

            if (response.IsSuccessStatusCode)
            {
                return RedirectToPage("./Index");
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Error occurred while update the flight class.");
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
            }

            return client;
        }
    }
}
