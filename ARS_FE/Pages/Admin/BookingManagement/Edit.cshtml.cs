using BusinessObjects.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace ARS_FE.Pages.Admin.BookingManagement
{
    public class EditModel : PageModel
    {
        private readonly DAO.AirlinesReservationSystemContext _context;

        public EditModel(DAO.AirlinesReservationSystemContext context)
        {
            _context = context;
        }

        [BindProperty]
        public BookingInformation BookingInformation { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bookinginformation = await _context.BookingInformations.FirstOrDefaultAsync(m => m.Id == id);
            if (bookinginformation == null)
            {
                return NotFound();
            }
            BookingInformation = bookinginformation;
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id");
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more information, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(BookingInformation).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BookingInformationExists(BookingInformation.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool BookingInformationExists(string id)
        {
            return _context.BookingInformations.Any(e => e.Id == id);
        }
    }
}
