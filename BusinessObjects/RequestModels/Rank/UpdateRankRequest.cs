using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObjects.RequestModels.Rank
{
    public class UpdateRankRequest
    {
        public string Id { get; set; }
        public string Type { get; set; } = null!;

        public string? Description { get; set; }

        public decimal Discount { get; set; }
    }
}
