using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using BusinessObjects.Models;
using DAO;
using System.Net.Http.Headers;
using System.Net.Http;
using BusinessObjects.RequestModels.Passenger;
using BusinessObjects.RequestModels.Airport;

namespace ARS_FE.Pages.UserPage.PassengerPage
{
    public class CreateModel : PageModel
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public CreateModel(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public void OnGet()
        {
            ViewData["Genders"] = new SelectList(new[] { "Male", "Female" });
            ViewData["Countries"] = new SelectList(new[] { "Việt Nam", "Singapore", "Thái Lan", "Malaysia" });
            ViewData["PassengerTypes"] = new SelectList(new[] { "Adult", "Child", "Infant" });
        }

        [BindProperty]
        public CreatePassengerRequest PassengerRequest { get; set; } = default!;

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var client = CreateAuthorizedClient();

            var newPassenger = new CreatePassengerRequest
            {
                FirstName = PassengerRequest.FirstName,
                LastName = PassengerRequest.LastName,
                Country = PassengerRequest.Country,
                Gender = PassengerRequest.Gender,
                Dob = PassengerRequest.Dob,
                Type = PassengerRequest.Type,
            };

            try
            {
                var response = await APIHelper.PostAsJson(client, "Passenger", newPassenger);
                if (response.IsSuccessStatusCode)
                {
                    return RedirectToPage("./Index");
                }
                else
                {
                    var errorContent = await response.Content.ReadAsStringAsync();
                    ModelState.AddModelError(string.Empty, errorContent);
                    return Page();
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
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
