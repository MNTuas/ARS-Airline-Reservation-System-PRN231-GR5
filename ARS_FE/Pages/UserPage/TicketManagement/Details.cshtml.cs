using BusinessObjects.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace ARS_FE.Pages.UserPage.TicketManagement
{
    public class DetailsModel : PageModel
    {
        private readonly DAO.AirlinesReservationSystemContext _context;

        public DetailsModel(DAO.AirlinesReservationSystemContext context)
        {
            _context = context;
        }

        public Ticket Ticket { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ticket = await _context.Tickets.FirstOrDefaultAsync(m => m.Id == id);
            if (ticket == null)
            {
                return NotFound();
            }
            else
            {
                Ticket = ticket;
            }
            return Page();
        }
    }
}
