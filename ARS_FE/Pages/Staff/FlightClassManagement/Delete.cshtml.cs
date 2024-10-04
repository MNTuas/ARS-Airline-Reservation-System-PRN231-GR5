using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BusinessObjects.Models;
using DAO;

namespace ARS_FE.Pages.Staff.FlightClassManagement
{
    public class DeleteModel : PageModel
    {
        private readonly DAO.AirlinesReservationSystemContext _context;

        public DeleteModel(DAO.AirlinesReservationSystemContext context)
        {
            _context = context;
        }

        [BindProperty]
        public FlightClass FlightClass { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var flightclass = await _context.FlightClasses.FirstOrDefaultAsync(m => m.Id == id);

            if (flightclass == null)
            {
                return NotFound();
            }
            else
            {
                FlightClass = flightclass;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var flightclass = await _context.FlightClasses.FindAsync(id);
            if (flightclass != null)
            {
                FlightClass = flightclass;
                _context.FlightClasses.Remove(FlightClass);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
