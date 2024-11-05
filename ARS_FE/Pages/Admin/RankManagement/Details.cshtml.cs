using BusinessObjects.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ARS_FE.Pages.UserPage.RankManagement
{
    public class DetailsModel : PageModel
    {

        public DetailsModel()
        {
        }

        public Rank Rank { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(string id)
        {
            if (id == null)
            {
                return NotFound();
            }


            return Page();
        }
    }
}
