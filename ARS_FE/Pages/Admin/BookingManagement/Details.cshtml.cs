using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BusinessObjects.Models;
using DAO;

namespace ARS_FE.Pages.Admin.BookingManagement
{
    public class DetailsModel : PageModel
    {
        private readonly DAO.AirlinesReservationSystemContext _context;

        public DetailsModel(DAO.AirlinesReservationSystemContext context)
        {
            _context = context;
        }

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
            else
            {
                BookingInformation = bookinginformation;
            }
            return Page();
        }
    }
}
