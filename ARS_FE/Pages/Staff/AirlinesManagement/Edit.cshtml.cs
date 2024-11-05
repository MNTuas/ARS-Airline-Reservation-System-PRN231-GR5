using BusinessObjects.Models;
using BusinessObjects.RequestModels.Airlines;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Net.Http.Headers;

namespace ARS_FE.Pages.Staff.AirlinesManagement
{
    public class EditModel : PageModel
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public EditModel(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        [BindProperty]
        public Airline Airline { get; set; } = default!;

        [BindProperty]
        public AirlinesUpdateModel airlinesUpdateModel { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(string id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var client = CreateAuthorizedClient();

            var response = await APIHelper.GetAsJsonAsync<Airline>(client, $"airline/{id}");
            if (response != null)
            {
                Airline = response;
                return Page();
            }
            else
            {
                return BadRequest();
            }
        }

        public async Task<IActionResult> OnPostAsync()
        {
            //if (!ModelState.IsValid)
            //{
            //    return Page();
            //}

            var client = CreateAuthorizedClient();

            var n = new AirlinesUpdateModel
            {
                Code = Airline.Code,
                Name = Airline.Name,
            };

            var response = await APIHelper.PutAsJson(client, $"airline/{Airline.Id}", n);

            if (response.IsSuccessStatusCode)
            {
                return RedirectToPage("./Index");
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Error occurred while update the airline.");
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
