using System;
using System.Collections.Generic;

namespace BusinessObjects.Models;

public partial class FlightClass
{
    public string Id { get; set; } = null!;

    public string? FlightId { get; set; }

    public string Class { get; set; } = null!;

    public int Quantity { get; set; }

    public decimal Price { get; set; }

    public virtual ICollection<BookingInformation> BookingInformations { get; set; } = new List<BookingInformation>();

    public virtual Flight? Flight { get; set; }
}
