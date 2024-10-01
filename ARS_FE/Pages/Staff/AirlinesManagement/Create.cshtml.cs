using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using BusinessObjects.Models;
using DAO;
using System.Text;
using System.Text.Json;

namespace ARS_FE.Pages.Staff.AirlinesManagement
{
    public class CreateModel : PageModel
    {

        public CreateModel()
        {
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public string AirlineName { get; set; } = default!;

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            using (var httpClient = new HttpClient())
            {
                var airlineName = AirlineName; 

                var response = await APIHelper.PostAsJson(httpClient, APIHelper.Url + "airline", airlineName);

                if (response.IsSuccessStatusCode)
                {
                    return RedirectToPage("./Index");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Error occurred while creating the airline.");
                    return Page();
                }
            }
        }

    }
}
