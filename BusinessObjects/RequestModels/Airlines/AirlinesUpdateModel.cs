using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObjects.RequestModels.Airlines
{
    public class AirlinesUpdateModel
    {
        [Required(ErrorMessage = "Please enter airlines name")]
        public string Name { get; set; } = null!;

        [Required(ErrorMessage = "Please enter airlines code")]
        public string Code { get; set; } = null!;
    }
}
