namespace BusinessObjects.RequestModels.Ticket
{
    using System.ComponentModel.DataAnnotations;

    public class CreateTicketRequest
    {
        public string? TicketClassId { get; set; }

        public string? BookingId { get; set; }

        [StringLength(50, ErrorMessage = "First name cannot be longer than 50 characters.")]
        [RegularExpression(@"^[\p{L}\s]+$", ErrorMessage = "Name can only contain letters and spacssses.")]
        public string? FirstName { get; set; }

        [StringLength(50, ErrorMessage = "Last name cannot be longer than 50 characters.")]
        [RegularExpression(@"^[\p{L}\s]+$", ErrorMessage = "Name can only contain letters and spacssses.")]
        public string? LastName { get; set; }

        public string? Gender { get; set; }


        public DateOnly Dob { get; set; }


        public string? Country { get; set; }
    }


}
