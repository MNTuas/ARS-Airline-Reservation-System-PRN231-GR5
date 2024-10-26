using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace BusinessObjects.RequestModels.Ticket
{
    public class CreateTicketRequest
    {
        public string TicketClassId { get; set; } = null!;

        public string BookingId { get; set; } = null!;

        public string FirstName { get; set; } = null!;

        public string LastName { get; set; } = null!;

        public string Gender { get; set; } = null!;

        //public DateOnly Dob { get; set; }

        public string Country { get; set; } = null!;
    }

}
