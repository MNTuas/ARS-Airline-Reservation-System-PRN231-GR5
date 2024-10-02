using BusinessObjects.Models;
using BusinessObjects.RequestModels.Airport;
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
                    Status = "Active"
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

        public async Task<List<Airport>> GetAllAirport()
        {
            return await _airportRepository.GetAllAirport();
        }

        public async Task<Airport> GetDetailsAirportInfo(string id)
        {
            return await _airportRepository.GetById(id);
        }


        public async Task UpdateAirports(string id, UpdateAirportRequest updateAirportRequest)
        {
            var airport = await _airportRepository.GetById(id);
            airport.Name = updateAirportRequest.Name;
            airport.City = updateAirportRequest.City;
            airport.Country = updateAirportRequest.Country;

            await _airportRepository.Update(airport);
        }

        public async Task ChangeAirportsStatus(string id, string status)
        {
            var Airport = await _airportRepository.GetById(id);
            Airport.Status = status;
            await _airportRepository.Update(Airport);
        }
    }
}

