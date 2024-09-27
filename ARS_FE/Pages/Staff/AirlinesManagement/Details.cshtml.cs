using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BusinessObjects.Models;
using DAO;

namespace ARS_FE.Pages.Staff.AirlinesManagement
{
    public class DetailsModel : PageModel
    {
        private readonly DAO.AirlinesReservationSystemContext _context;

        public DetailsModel(DAO.AirlinesReservationSystemContext context)
        {
            _context = context;
        }

        public Airline Airline { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var airline = await _context.Airlines.FirstOrDefaultAsync(m => m.Id == id);
            if (airline == null)
            {
                return NotFound();
            }
            else
            {
                Airline = airline;
            }
            return Page();
        }
    }
}
