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

namespace ARS_FE.Pages.Staff.AirlinesManagement
{
    public class EditModel : PageModel
    {
        public EditModel()
        {
        }

        [BindProperty]
        public Airline Airline { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            using (var httpClient = new HttpClient())
            {

                var response = await APIHelper.GetAsJsonAsync<Airline>(httpClient, APIHelper.Url + $"airline/{id}");

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
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            using (var httpClient = new HttpClient())
            {
                var airlineName = Airline.Name;

                var response = await APIHelper.PutAsJson(httpClient, APIHelper.Url + $"airline/{Airline.Id}", airlineName);

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
        }
    }
}
