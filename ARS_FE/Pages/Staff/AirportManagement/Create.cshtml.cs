using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using BusinessObjects.Models;
using DAO;
using BusinessObjects.RequestModels.Airport;

namespace ARS_FE.Pages.Staff.AirportManagement
{
    public class CreateModel : PageModel
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public CreateModel(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public CreateAirportRequest createAirportRequest { get; set; } = default!;

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            var client = _httpClientFactory.CreateClient("ApiClient");

            var n = new CreateAirportRequest
            {
                Name = createAirportRequest.Name,
                City = createAirportRequest.City,
                Country = createAirportRequest.Country,
            };

            var response = await APIHelper.PostAsJson(client, "Airport", n);

            if (response.IsSuccessStatusCode)
            {
                return RedirectToPage("./Index");
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Error occurred while creating the Airport.");
                return Page();
            }

        }

    }
}
