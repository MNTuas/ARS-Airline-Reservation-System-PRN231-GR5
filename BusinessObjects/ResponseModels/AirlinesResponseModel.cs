using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObjects.ResponseModels
{
    public class AirlinesResponseModel
    {
        public string Id { get; set; } = null!;

        public string Name { get; set; } = null!;

        public string Status { get; set; } = null!;

        public List<AirplaneResponseModel> Airplanes { get; set; } = new List<AirplaneResponseModel>();
    }

    public class AirplaneResponseModel
    {
        public string Id { get; set; } = null!;

        public string Code { get; set; } = null!;

        public string Type { get; set; } = null!;

        public int AvailableSeat { get; set; }
    }
}
