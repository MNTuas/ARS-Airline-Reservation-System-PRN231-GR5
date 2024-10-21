using System;
using System.Collections.Generic;

namespace BusinessObjects.Models;

public partial class Ticket
{
    public string Id { get; set; } = null!;

    public string TicketClassId { get; set; } = null!;

    public string PassengerId { get; set; } = null!;

    public string BookingId { get; set; } = null!;

    public bool Status { get; set; }

    public virtual BookingInformation Booking { get; set; } = null!;

    public virtual Passenger Passenger { get; set; } = null!;

    public virtual TicketClass TicketClass { get; set; } = null!;
}
