using System;
using System.Collections.Generic;

namespace BusinessObjects.Models;

public partial class Flight
{
    public string Id { get; set; } = null!;

    public string? AirplaneId { get; set; }

    public DateTime DepartureTime { get; set; }

    public DateTime ArrivalTime { get; set; }

    public string Status { get; set; } = null!;

    public string? From { get; set; }

    public string? To { get; set; }

    public virtual Airplane? Airplane { get; set; }

    public virtual ICollection<BookingInformation> BookingInformations { get; set; } = new List<BookingInformation>();

    public virtual ICollection<FlightClass> FlightClasses { get; set; } = new List<FlightClass>();

    public virtual Airport? FromNavigation { get; set; }

    public virtual Airport? ToNavigation { get; set; }
}
