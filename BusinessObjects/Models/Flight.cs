namespace BusinessObjects.Models;

public partial class Flight
{
    public string Id { get; set; } = null!;

    public string FlightNumber { get; set; } = null!;

    public string AirplaneId { get; set; } = null!;

    public DateTime DepartureTime { get; set; }

    public DateTime ArrivalTime { get; set; }

    public int Duration { get; set; }

    public string Status { get; set; } = null!;

    public string From { get; set; } = null!;

    public string To { get; set; } = null!;

    public virtual Airplane Airplane { get; set; } = null!;

    public virtual Airport FromNavigation { get; set; } = null!;

    public virtual ICollection<TicketClass> TicketClasses { get; set; } = new List<TicketClass>();

    public virtual Airport ToNavigation { get; set; } = null!;
}
