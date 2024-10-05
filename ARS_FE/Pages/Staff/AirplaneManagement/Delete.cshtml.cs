using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BusinessObjects.Models;
using DAO;

namespace ARS_FE.Pages.Staff.AirplaneManagement
{
    public class DeleteModel : PageModel
    {
        private readonly DAO.AirlinesReservationSystemContext _context;

        public DeleteModel(DAO.AirlinesReservationSystemContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Airplane Airplane { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var airplane = await _context.Airplanes.FirstOrDefaultAsync(m => m.Id == id);

            if (airplane == null)
            {
                return NotFound();
            }
            else
            {
                Airplane = airplane;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var airplane = await _context.Airplanes.FindAsync(id);
            if (airplane != null)
            {
                Airplane = airplane;
                _context.Airplanes.Remove(Airplane);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
