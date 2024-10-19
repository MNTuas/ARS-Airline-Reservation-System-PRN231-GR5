using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObjects.ResponseModels.Flight
{
    public class FlightResponseModel
    {
        public string Id { get; set; } = null!;

        public string FlightNumber { get; set; } = null!;

        public string AirlinesId { get; set; } = null!;

        public string Airlines { get; set; } = null!;

        public string? AirplaneId { get; set; }

        public string? AirplaneCode { get; set; }

        public DateTime DepartureTime { get; set; }

        public DateTime ArrivalTime { get; set; }

        public int Duration { get; set; }

        public string Status { get; set; } = null!;

        public string? From { get; set; }

        public string? FromName { get; set; }

        public string? To { get; set; }

        public string? ToName { get; set; }

        public List<TicketClassPriceResponse> TicketClassPrices { get; set; } = new List<TicketClassPriceResponse>();
    }

    public class TicketClassPriceResponse
    {
        public string Id { get; set; } = null!;

        public string SeatClassName { get; set; } = null!;

        public string SeatClassId { get; set; } = null!;

        public decimal Price { get; set; }
    }
}
