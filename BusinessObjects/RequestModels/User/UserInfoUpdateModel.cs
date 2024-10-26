using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObjects.RequestModels.User
{
    public class UserInfoUpdateModel
    {
        [Required]
        public string Name { get; set; } = null!;

        public string? PhoneNumber { get; set; }

        [Required]
        public string Email { get; set; } = null!;

        public string? Address { get; set; }
    }
}
