using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using BusinessObjects.Models;
using DAO;

namespace ARS_FE.Pages.Admin.BookingManagement
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
