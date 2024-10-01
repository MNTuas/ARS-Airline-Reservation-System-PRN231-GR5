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

namespace ARS_FE.Pages.Staff.AirportManagement
{
    public class EditModel : PageModel
    {
        private readonly DAO.AirlinesReservationSystemContext _context;

        public EditModel(DAO.AirlinesReservationSystemContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Airport Airport { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var airport =  await _context.Airports.FirstOrDefaultAsync(m => m.Id == id);
            if (airport == null)
            {
                return NotFound();
            }
            Airport = airport;
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

            _context.Attach(Airport).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AirportExists(Airport.Id))
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

        private bool AirportExists(string id)
        {
            return _context.Airports.Any(e => e.Id == id);
        }
    }
}
