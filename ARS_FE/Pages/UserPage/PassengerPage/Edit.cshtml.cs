using BusinessObjects.Models;
using BusinessObjects.RequestModels.Passenger;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Net.Http.Headers;
using System.Text.Json;
using static ARS_FE.Pages.UserPage.PassengerPage.CreateModel;

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
        [BindProperty(SupportsGet = true)]
        public string PassengerId { get; set; } = default!;
        public List<Country> Countries { get; set; } = new List<Country>();
        public async Task<IActionResult> OnGetAsync(string id)
        {
            ViewData["Genders"] = new SelectList(new[] { "Male", "Female" });
            await LoadCountriesAsync();
            updatePassengerRequest = new UpdatePassengerRequest();
            updatePassengerRequest.Dob = DateOnly.FromDateTime(DateTime.Today);
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
                };
                return Page();
            }

            return BadRequest();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                await LoadCountriesAsync();
                ViewData["Genders"] = new SelectList(new[] { "Male", "Female" });
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
                    ViewData["Genders"] = new SelectList(new[] { "Male", "Female" });
                    await LoadCountriesAsync();
                    return Page();
                }

                return RedirectToPage("./Index");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, $"Exception: {ex.Message}");
                ViewData["Genders"] = new SelectList(new[] { "Male", "Female" });
                await LoadCountriesAsync();
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

        private async Task LoadCountriesAsync()
        {
            var client = _httpClientFactory.CreateClient();
            var response = await client.GetAsync("https://restcountries.com/v3.1/all");

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                Console.WriteLine(content); // In ra để kiểm tra

                var options = new JsonSerializerOptions
                {
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                    PropertyNameCaseInsensitive = true
                };

                try
                {
                    Countries = JsonSerializer.Deserialize<List<Country>>(content, options);
                    if (Countries != null)
                    {
                        Console.WriteLine($"Total countries: {Countries.Count}");
                    }
                    else
                    {
                        Console.WriteLine("Countries is null.");
                    }
                }
                catch (JsonException ex)
                {
                    Console.WriteLine($"JSON Error: {ex.Message}");
                }
            }
            else
            {
                Console.WriteLine($"Error: {response.StatusCode}");
            }
        }

        public class Country
        {
            public Name Name { get; set; }
        }
        public class Name
        {
            public string Common { get; set; }
        }
    }
}
