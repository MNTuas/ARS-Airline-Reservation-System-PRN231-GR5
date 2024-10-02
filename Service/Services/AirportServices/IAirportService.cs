using BusinessObjects.Models;
using BusinessObjects.RequestModels;
using BusinessObjects.ResponseModels;
using FFilms.Application.Shared.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services.AirportService
{
    public interface IAirportService
    {
        Task<List<AirportResponseModel>> GetAllAirport();
        Task<Result<Airport>> AddAirport(CreateAirportRequest createAirportRequest);
    }
}
