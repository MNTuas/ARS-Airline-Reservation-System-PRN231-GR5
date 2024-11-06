using BusinessObjects.Models;
using BusinessObjects.RequestModels.Airplane;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Net.Http.Headers;

namespace ARS_FE.Pages.Staff.AirplaneManagement
{
    public class EditModel : PageModel
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public EditModel(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        [BindProperty]
        public string Id { get; set; } = null!;

        [BindProperty]
        public UpdateAirplaneRequest UpdateAirplane { get; set; } = new UpdateAirplaneRequest();

        public async Task<IActionResult> OnGetAsync(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return NotFound();
            }

            var client = CreateAuthorizedClient();
            var response = await APIHelper.GetAsJsonAsync<Airplane>(client, $"airplane/get-airplane/{id}");

            if (response != null)
            {
                UpdateAirplane = new UpdateAirplaneRequest
                {
                    CodeNumber = response.CodeNumber,
                    AirplaneSeatRequest = response.AirplaneSeats.Select(seat => new AirplaneSeatUpdateRequest
                    {
                        SeatClassId = seat.SeatClassId,
                        SeatCount = seat.SeatCount
                    }).ToList()
                };

                var seatClasses = await APIHelper.GetAsJsonAsync<List<SeatClass>>(client, "seat-class");
                var seatClassesNames = seatClasses?.ToDictionary(sc => sc.Id.ToString(), sc => sc.Name);
                ViewData["SeatClassesNames"] = seatClassesNames;

                return Page();
            }
            else
            {
                return BadRequest("Airplane not found.");
            }
        }
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            try
            {
                var client = CreateAuthorizedClient();
                var response = await APIHelper.PutAsJson(client, $"airplane/update-airplane/{Id}", UpdateAirplane);

                if (response.IsSuccessStatusCode)
                {
                    return RedirectToPage("./Index");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Error occurred while updating the airplane.");
                    return Page();
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, $"An error occurred: {ex.Message}");
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
