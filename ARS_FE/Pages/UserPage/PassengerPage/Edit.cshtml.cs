using BusinessObjects.Models;
using BusinessObjects.RequestModels.Passenger;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Net.Http.Headers;

namespace ARS_FE.Pages.UserPage.PassengerPage
{
    public class EditModel : PageModel
    {
        private readonly IHttpClientFactory _httpClientFactory;
        public EditModel(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        [BindProperty]
        public UpdatePassengerRequest updatePassengerRequest { get; set; } = default!;
        [BindProperty]
        public string PassengerId { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(string id)
        {
            ViewData["Genders"] = new SelectList(new[] { "Male", "Female" });
            ViewData["Countries"] = new SelectList(new[] { "Việt Nam", "Singapore", "Thái Lan", "Malaysia" });
            ViewData["PassengerTypes"] = new SelectList(new[] { "Adult", "Child", "Infant" });

            if (id == null)
            {
                return NotFound();
            }

            var client = CreateAuthorizedClient();
            var response = await APIHelper.GetAsJsonAsync<Passenger>(client, $"Passenger/{id}");

            if (response != null)
            {
                PassengerId = id;
                updatePassengerRequest = new UpdatePassengerRequest
                {
                    FirstName = response.FirstName,
                    LastName = response.LastName,
                    Gender = response.Gender,
                    Country = response.Country,
                    Dob = response.Dob,
                    Type = response.Type
                };
                return Page();
            }

            return BadRequest();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            ViewData["Genders"] = new SelectList(new[] { "Male", "Female" });
            ViewData["Countries"] = new SelectList(new[] { "Việt Nam", "Singapore", "Thái Lan", "Malaysia" });
            ViewData["PassengerTypes"] = new SelectList(new[] { "Adult", "Child", "Infant" });

            if (!ModelState.IsValid)
            {
                return Page();
            }

            var client = CreateAuthorizedClient();
            try
            {
                var response = await APIHelper.PutAsJson(client, $"Passenger/{PassengerId}", updatePassengerRequest);
                var responseContent = await response.Content.ReadAsStringAsync();

                if (!response.IsSuccessStatusCode)
                {
                    ModelState.AddModelError(string.Empty, $"API Response: {responseContent}");
                    return Page();
                }

                return RedirectToPage("./Index");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, $"Exception: {ex.Message}");
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
