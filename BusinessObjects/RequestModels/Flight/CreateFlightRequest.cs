using System.ComponentModel.DataAnnotations;

namespace BusinessObjects.RequestModels.Flight
{
    public class CreateFlightRequest
    {
        [Required(ErrorMessage = "Please enter flight number")]
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

        [Required(ErrorMessage = "Please enter ticket class price")]
        public decimal Price { get; set; }
    }
}
