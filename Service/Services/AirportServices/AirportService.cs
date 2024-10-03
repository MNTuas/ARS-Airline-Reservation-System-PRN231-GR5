using BusinessObjects.Models;
using BusinessObjects.RequestModels;
using BusinessObjects.ResponseModels;
using FFilms.Application.Shared.Response;
using Repository.Repositories.AirporRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services.AirportService
{
    public class AirportService : IAirportService
    {
        private readonly IAirportRepository _airportRepository;

        public AirportService(IAirportRepository airportRepository)
        {
            _airportRepository = airportRepository;
        }
        public async Task<Result<Airport>> AddAirport(CreateAirportRequest createAirportRequest)
        {
            try
            {
                var newAirport = new Airport
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = createAirportRequest.Name,
                    City = createAirportRequest.City,
                    Country = createAirportRequest.Country,
                    Status = "ACTIVE"
                };

                await _airportRepository.Insert(newAirport);

                return new Result<Airport>
                {
                    Success = true,
                    Message = "Create successful!",
                    Data = newAirport
                };

            }
            catch (Exception ex)
            {
                return new Result<Airport>
                {
                    Success = false,
                    Message = "Something wrong!!!",
                };
            }
        }

        public async Task<List<AirportResponseModel>> GetAllAirport()
        {
            var result = await _airportRepository.GetAllAirport();
            return result.Select(r => new AirportResponseModel
            {
                Id= r.Id,
                Name = r.Name,
                City = r.City,
                Country = r.Country,
                Status = r.Status,
            }).ToList();
        }
    }
}
