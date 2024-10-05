using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObjects.ResponseModels
{
    public class FlightResponseModel
    {
        public string Id { get; set; } = null!;

        public string AirlinesId { get; set; } = null!;

        public string Airlines { get; set; } = null!;

        public string? AirplaneId { get; set; }

        public string? AirplaneCode { get; set; }

        public DateTime DepartureTime { get; set; }

        public DateTime ArrivalTime { get; set; }

        public string Status { get; set; } = null!;

        public string? FromId { get; set; }

        public string? From { get; set; }

        public string? ToId { get; set; }

        public string? To { get; set; }
    }
}
