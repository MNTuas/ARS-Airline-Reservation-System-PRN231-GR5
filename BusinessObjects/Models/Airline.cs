using System;
using System.Collections.Generic;

namespace BusinessObjects.Models;

public partial class Airline
{
    public string Id { get; set; } = null!;

    public string Name { get; set; } = null!;

    public string Code { get; set; } = null!;

    public bool Status { get; set; }

    public virtual ICollection<Airplane> Airplanes { get; set; } = new List<Airplane>();
}
