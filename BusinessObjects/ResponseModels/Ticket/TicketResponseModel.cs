namespace BusinessObjects.ResponseModels.Ticket
{
    public class TicketResponseModel
    {
        public string Id { get; set; } = null!;

        public string ClassName { get; set; } = null!;

        public string ClassPrice { get; set; } = null!;

        public string BookingId { get; set; } = null!;

        public string FirstName { get; set; } = null!;

        public string LastName { get; set; } = null!;

        public string Gender { get; set; } = null!;

        public DateOnly Dob { get; set; }

        public string Country { get; set; } = null!;

        public string Status { get; set; } = null!;
    }
}
