using BusinessObjects.Models;
using BusinessObjects.ResponseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services.FlightServices
{
    public interface IFlightService
    {
        Task<List<Flight>> GetAllFlight();
        Task<List<FlightResponseModel>> GetAllFlightsDetails();
        Task<FlightResponseModel> GetFlightById(string id);
    }
}
