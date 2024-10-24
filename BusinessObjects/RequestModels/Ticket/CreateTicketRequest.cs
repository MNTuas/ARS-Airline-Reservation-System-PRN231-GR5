using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace BusinessObjects.RequestModels.Ticket
{
    public class CreateTicketRequest
    {
        public string? TicketClassId { get; set; } 

        public string? BookingId { get; set; }

        public string? FirstName { get; set; }

        public string? LastName { get; set; }

        public string? Gender { get; set; } 

        [DataType(DataType.Date)]
        public DateOnly Dob { get; set; }

        public string? Country { get; set; }
    }

}
