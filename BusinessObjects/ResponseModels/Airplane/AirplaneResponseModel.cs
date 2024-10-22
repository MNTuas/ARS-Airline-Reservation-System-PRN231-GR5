using BusinessObjects.Models;
using BusinessObjects.ResponseModels.Flight;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
