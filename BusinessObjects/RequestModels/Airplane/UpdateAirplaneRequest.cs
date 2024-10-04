using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObjects.RequestModels.Airplane
{
    public class UpdateAirplaneRequest
    {
        public string Id { get; set; } = null!;

        public string Code { get; set; } = null!;

        public string Type { get; set; } = null!;

        public int AvailableSeat { get; set; }

        public string? AirlinesId { get; set; }

        public string? Status { get; set; }
    }
}
