using System;
using System.Collections.Generic;

namespace BusinessObjects.Models;

public partial class Route
{
    public string Id { get; set; } = null!;

    public string From { get; set; } = null!;

    public string To { get; set; } = null!;

    public virtual ICollection<Flight> Flights { get; set; } = new List<Flight>();

    public virtual Airport FromNavigation { get; set; } = null!;

    public virtual Airport ToNavigation { get; set; } = null!;
}
