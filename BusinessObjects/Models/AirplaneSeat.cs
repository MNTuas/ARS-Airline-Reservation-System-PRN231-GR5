namespace BusinessObjects.Models;

public partial class AirplaneSeat
{
    public string Id { get; set; } = null!;

    public string AirplaneId { get; set; } = null!;

    public string SeatClassId { get; set; } = null!;

    public int SeatCount { get; set; }

    public virtual Airplane Airplane { get; set; } = null!;

    public virtual SeatClass SeatClass { get; set; } = null!;
}
