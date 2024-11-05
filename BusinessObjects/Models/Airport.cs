namespace BusinessObjects.Models;

public partial class Airport
{
    public string Id { get; set; } = null!;

    public string Name { get; set; } = null!;

    public string City { get; set; } = null!;

    public string Country { get; set; } = null!;

    public bool Status { get; set; }

    public virtual ICollection<Flight> FlightFromNavigations { get; set; } = new List<Flight>();

    public virtual ICollection<Flight> FlightToNavigations { get; set; } = new List<Flight>();
}
