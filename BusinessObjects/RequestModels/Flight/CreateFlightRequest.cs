using System.ComponentModel.DataAnnotations;

namespace BusinessObjects.RequestModels.Flight
{
    public class CreateFlightRequest
    {
        [Required(ErrorMessage = "Please enter flight number")]
        [RegularExpression("^[0-9]+$", ErrorMessage = "Flight number must contain only numbers")]
        public string FlightNumber { get; set; } = null!;

        [Required(ErrorMessage = "Please choose airplane")]
        public string AirplaneId { get; set; } = null!;

        [Required(ErrorMessage = "Please choose departure time")]
        public DateTime DepartureTime { get; set; }

        [Required(ErrorMessage = "Please input flight duration")]
        public int Duration { get; set; }

        [Required(ErrorMessage = "Please choose origin")]
        public string From { get; set; } = null!;

        [Required(ErrorMessage = "Please choose destination")]
        public string To { get; set; } = null!;

        public List<TicketClassPrice> TicketClassPrices { get; set; } = new List<TicketClassPrice>();
    }

    public class TicketClassPrice
    {
        public string SeatClassId { get; set; } = null!;

        public string SeatClassName { get; set; } = null!;

        public int TotalSeat { get; set; }

        public int RemainSeat { get; set; }

        [Required(ErrorMessage = "Please enter ticket class price")]
        [Range(700000, 50000000, ErrorMessage = "The price must be between 700000 and 50000000!")]
        public decimal Price { get; set; }
    }
}
