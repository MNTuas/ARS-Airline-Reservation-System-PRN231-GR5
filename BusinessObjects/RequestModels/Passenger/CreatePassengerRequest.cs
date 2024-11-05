using System.ComponentModel.DataAnnotations;

namespace BusinessObjects.RequestModels.Passenger
{
    public class CreatePassengerRequest
    {
        [Required(ErrorMessage = "Vui lòng nhập họ")]
        public string FirstName { get; set; } = null!;
        [Required(ErrorMessage = "Vui lòng nhập tên")]
        public string LastName { get; set; } = null!;
        [Required(ErrorMessage = "Vui lòng chọn giới tính")]
        public string Gender { get; set; } = null!;
        [Required(ErrorMessage = "Vui lòng nhập ngày sinh")]
        public DateOnly Dob { get; set; }
        [Required(ErrorMessage = "Vui lòng chọn quốc tịch")]
        public string Country { get; set; } = null!;

        public string Type { get; set; } = null!;

    }
}
