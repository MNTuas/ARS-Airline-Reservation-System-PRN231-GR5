using BusinessObjects.Models;
using BusinessObjects.ResponseModels.Ticket;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObjects.ResponseModels.Booking
{
    public class UserBookingResponseModel
    {
        public string Id { get; set; } = null!;

        public DateTime CreatedDate { get; set; }

        public int Quantity { get; set; }

        public string Status { get; set; } = null!;

        public virtual List<TicketResponseModel> Tickets { get; set; } = new List<TicketResponseModel>();

    }


}
