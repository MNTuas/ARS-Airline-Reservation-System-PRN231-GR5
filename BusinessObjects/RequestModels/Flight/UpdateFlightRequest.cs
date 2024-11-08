using System.ComponentModel.DataAnnotations;

namespace BusinessObjects.RequestModels.Flight
{
    public class UpdateFlightRequest
    {
        [Required(ErrorMessage = "Please choose flight id")]
        public string FlightId { get; set; } = null!;

        [Required(ErrorMessage = "Please choose departure time")]
        public DateTime DepartureTime { get; set; }

        [Required(ErrorMessage = "Please input flight duration")]
        public int Duration { get; set; }

        [Required(ErrorMessage = "Please choose origin")]
        public string From { get; set; } = null!;

        [Required(ErrorMessage = "Please choose destination")]
        public string To { get; set; } = null!;

        public List<TicketClassPriceUpdate> TicketClassPrices { get; set; } = new List<TicketClassPriceUpdate>();
    }

    public class TicketClassPriceUpdate
    {
        public string Id { get; set; } = null!;

        public string? SeatClassName { get; set; }

        [Required(ErrorMessage = "Please enter ticket class price")]
        public decimal Price { get; set; }
    }
}
