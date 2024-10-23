using System;
using System.Collections.Generic;

namespace BusinessObjects.Models;

public partial class SeatClass
{
    public string Id { get; set; } = null!;

    public string Name { get; set; } = null!;

    public string? Description { get; set; }

    public bool Status { get; set; }

    public virtual ICollection<AirplaneSeat> AirplaneSeats { get; set; } = new List<AirplaneSeat>();

    public virtual ICollection<TicketClass> TicketClasses { get; set; } = new List<TicketClass>();
}
