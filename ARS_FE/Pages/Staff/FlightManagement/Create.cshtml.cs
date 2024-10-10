using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using BusinessObjects.Models;
using DAO;

namespace ARS_FE.Pages.Staff.FlightManagement
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
        ViewData["AirplaneId"] = new SelectList(_context.Airplanes, "Id", "Id");
        ViewData["From"] = new SelectList(_context.Airports, "Id", "Id");
        ViewData["To"] = new SelectList(_context.Airports, "Id", "Id");
            return Page();
        }

        [BindProperty]
        public Flight Flight { get; set; } = default!;

        // For more information, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Flights.Add(Flight);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
