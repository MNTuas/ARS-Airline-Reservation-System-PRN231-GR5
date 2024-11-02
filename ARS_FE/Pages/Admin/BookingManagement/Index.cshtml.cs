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
    public class IndexModel : PageModel
    {
        private readonly DAO.AirlinesReservationSystemContext _context;

        public IndexModel(DAO.AirlinesReservationSystemContext context)
        {
            _context = context;
        }

        public IList<BookingInformation> BookingInformation { get;set; } = default!;

        public async Task OnGetAsync()
        {
            BookingInformation = await _context.BookingInformations
                .Include(b => b.User).ToListAsync();
        }
    }
}
