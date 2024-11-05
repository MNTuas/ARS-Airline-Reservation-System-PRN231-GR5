using BusinessObjects.ResponseModels.Flight;

namespace BusinessObjects.ResponseModels.Airplane
{
    public class AirplaneResponseModel
    {
        public string Id { get; set; } = null!;

        public string CodeNumber { get; set; } = null!;

        public bool Status { get; set; }
        public List<FlightResponseModel> Flights { get; set; }

        //public virtual ICollection<AirplaneSeat> AirplaneSeats { get; set; } = new List<AirplaneSeat>();

        public virtual List<AirplaneSeatResponse> AirplaneSeats { get; set; } = new List<AirplaneSeatResponse>();

    }

    public class AirplaneSeatResponse
    {
        public string Id { get; set; } = null!;

        public string SeatClassId { get; set; } = null!;

        public string SeatClassName { get; set; } = null!;

        public int SeatCount { get; set; }
    }
}
