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
        //[JsonIgnore]
        //public List<string> PassengerIds { get; set; } = new List<string>(); // Danh sách ID của hành khách

        public string TicketClassId { get; set; } = null!;
    }

}
