namespace BusinessObjects.ResponseModels.Passenger
{
    public class PassengerResposeModel
    {
        public string Id { get; set; } = null!;
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string Gender { get; set; } = null!;
        public DateOnly Dob { get; set; }
        public string Country { get; set; } = null!;
        public string Type { get; set; } = null!;
    }
}
