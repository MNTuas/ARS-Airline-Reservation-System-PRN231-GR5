using BusinessObjects.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ARS_FE.Pages.UserPage.BookingManager
{
    public class CreateModel : PageModel
    {
        private readonly DAO.AirlinesReservationSystemContext _context;

        public CreateModel(DAO.AirlinesReservationSystemContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id");
            return Page();
        }

        [BindProperty]
        public BookingInformation BookingInformation { get; set; } = default!;

        // For more information, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.BookingInformations.Add(BookingInformation);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
