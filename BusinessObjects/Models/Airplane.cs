using System;
using System.Collections.Generic;

namespace BusinessObjects.Models;

public partial class Airplane
{
    public string Id { get; set; } = null!;

    public string Code { get; set; } = null!;

    public string Type { get; set; } = null!;

    public int AvailableSeat { get; set; }

    public string? AirlinesId { get; set; }

    public string? Status { get; set; }

    public virtual Airline? Airlines { get; set; }

    public virtual ICollection<Flight> Flights { get; set; } = new List<Flight>();
}
