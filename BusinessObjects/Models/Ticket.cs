namespace BusinessObjects.Models;

public partial class Ticket
{
    public string Id { get; set; } = null!;

    public string TicketClassId { get; set; } = null!;

    public string BookingId { get; set; } = null!;

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public string Gender { get; set; } = null!;

    public DateOnly Dob { get; set; }

    public string Country { get; set; } = null!;

    public string Status { get; set; } = null!;

    public virtual BookingInformation Booking { get; set; } = null!;

    public virtual TicketClass TicketClass { get; set; } = null!;
}
