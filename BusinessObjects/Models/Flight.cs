using System;
using System.Collections.Generic;

namespace BusinessObjects.Models;

public partial class Flight
{
    public string Id { get; set; } = null!;

    public string AirplaneId { get; set; } = null!;

    public DateTime DepartureTime { get; set; }

    public DateTime ArrivalTime { get; set; }

    public string RouteId { get; set; } = null!;

    public string Status { get; set; } = null!;

    public virtual Airplane Airplane { get; set; } = null!;

    public virtual ICollection<BookingInformation> BookingInformations { get; set; } = new List<BookingInformation>();

    public virtual ICollection<FlightClass> FlightClasses { get; set; } = new List<FlightClass>();

    public virtual Route Route { get; set; } = null!;
}
