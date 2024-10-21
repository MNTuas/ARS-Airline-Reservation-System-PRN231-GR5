using BusinessObjects.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services.SeatClassServices
{
    public interface ISeatClassService
    {
        Task<List<SeatClass>> GetAllSeatClass();
    }
}
