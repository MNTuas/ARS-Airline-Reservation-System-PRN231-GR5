using System;
using System.Collections.Generic;

namespace BusinessObjects.Models;

public partial class Airport
{
    public string Id { get; set; } = null!;

    public string Name { get; set; } = null!;

    public string City { get; set; } = null!;

    public string Country { get; set; } = null!;

    public string Status { get; set; } = null!;

    public virtual ICollection<Route> RouteFromNavigations { get; set; } = new List<Route>();

    public virtual ICollection<Route> RouteToNavigations { get; set; } = new List<Route>();
}
