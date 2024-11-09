using BusinessObjects.RequestModels.Passenger;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Net.Http.Headers;
using System.Text.Json;

namespace ARS_FE.Pages.UserPage.PassengerPage
{
    public class CreateModel : PageModel
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public CreateModel(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }
        public List<Country> Countries { get; set; } = new List<Country>();

        [BindProperty]
        public CreatePassengerRequest PassengerRequest { get; set; } = default!;

        public async Task OnGetAsync()
        {
            ViewData["Genders"] = new SelectList(new[] { "Male", "Female" });
            await LoadCountriesAsync();
            PassengerRequest = new CreatePassengerRequest();
            PassengerRequest.Dob = DateOnly.FromDateTime(DateTime.Today);
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

            var newPassenger = new CreatePassengerRequest
            {
                FirstName = PassengerRequest.FirstName,
                LastName = PassengerRequest.LastName,
                Country = PassengerRequest.Country,
                Gender = PassengerRequest.Gender,
                Dob = PassengerRequest.Dob,
            };

            try
            {
                var response = await APIHelper.PostAsJson(client, "Passenger", newPassenger);
                if (response.IsSuccessStatusCode)
                {
                    await LoadCountriesAsync();
                    return RedirectToPage("./Index");
                }
                else
                {
                    var errorContent = await response.Content.ReadAsStringAsync();
                    ModelState.AddModelError(string.Empty, errorContent);
                    ViewData["Genders"] = new SelectList(new[] { "Male", "Female" });
                    await LoadCountriesAsync();
                    return Page();
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
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
