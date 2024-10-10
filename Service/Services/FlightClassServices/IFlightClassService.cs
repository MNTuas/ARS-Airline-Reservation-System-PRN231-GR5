using BusinessObjects.Models;
using BusinessObjects.RequestModels;
using BusinessObjects.ResponseModels;
using FFilms.Application.Shared.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services.FlightClassServices
{
    public interface IFlightClassService
    {
        Task<FlightClass> GetFlightById(string id);
        Task<List<FlightClassResponseModel>> GetAllFlightClassse();
        Task<Result<FlightClass>> AddFlightClass(FlightClassRequest request);
        Task<Result<FlightClass>> UpdateFlightClass(FlightClassRequest request, string id);
        Task<Result<FlightClass>> DeleteFlightClass(string id);
    }
}
