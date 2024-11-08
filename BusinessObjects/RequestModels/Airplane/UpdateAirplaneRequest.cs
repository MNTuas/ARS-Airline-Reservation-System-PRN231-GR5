using System.ComponentModel.DataAnnotations;

namespace BusinessObjects.RequestModels.Airplane
{
    public class UpdateAirplaneRequest
    {
        [Required(ErrorMessage = "Please enter airplane code number.")]
        [RegularExpression(@"^[1-9]\d*$", ErrorMessage = "Code number must start with a non-zero digit and contain only numeric characters.")]
        public string CodeNumber { get; set; } = null!;

        [Required(ErrorMessage = "Please enter amount of seat for each class.")]
        public virtual List<AirplaneSeatUpdateRequest> AirplaneSeatRequest { get; set; } = new List<AirplaneSeatUpdateRequest>();
    }

    public class AirplaneSeatUpdateRequest
    {
        [Required(ErrorMessage = "Please choose seat class.")]
        public string SeatClassId { get; set; } = null!;

        [Required(ErrorMessage = "Please enter seat count.")]
        public int SeatCount { get; set; }
    }
}
