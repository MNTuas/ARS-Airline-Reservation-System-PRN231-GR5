using System;
using System.Collections.Generic;

namespace BusinessObjects.Models;

public partial class Airplane
{
    public string Id { get; set; } = null!;

    public string CodeNumber { get; set; } = null!;

    public bool Status { get; set; }

    public string AirlinesId { get; set; } = null!;

    public virtual Airline Airlines { get; set; } = null!;

    public virtual ICollection<AirplaneSeat> AirplaneSeats { get; set; } = new List<AirplaneSeat>();

    public virtual ICollection<Flight> Flights { get; set; } = new List<Flight>();
}
