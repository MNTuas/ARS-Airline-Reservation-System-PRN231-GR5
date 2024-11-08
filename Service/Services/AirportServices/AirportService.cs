using AutoMapper;
using BusinessObjects.Models;
using BusinessObjects.RequestModels.Airport;
using BusinessObjects.ResponseModels.Airport;
using FFilms.Application.Shared.Response;
using Repository.Repositories.AirporRepositories;

namespace Service.Services.AirportService
{
    public class AirportService : IAirportService
    {
        private readonly IAirportRepository _airportRepository;
        private readonly IMapper _mapper;

        public AirportService(IAirportRepository airportRepository, IMapper mapper)
        {
            _airportRepository = airportRepository;
            _mapper = mapper;
        }
        public async Task<Result<Airport>> AddAirport(CreateAirportRequest createAirportRequest)
        {
            try
            {
                var newAirport = _mapper.Map<Airport>(createAirportRequest);

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
                    Message = ex.Message,
                };
            }
        }

        public async Task<List<AirportResponseModel>> GetAllAirport()
        {
            var result = await _airportRepository.GetAllAirport();
            return _mapper.Map<List<AirportResponseModel>>(result);
        }

        public async Task<Airport> GetDetailsAirportInfo(string id)
        {
            return await _airportRepository.GetById(id);
        }

        public async Task UpdateAirports(string id, UpdateAirportRequest updateAirportRequest)
        {
            var airport = await _airportRepository.GetById(id);
            _mapper.Map(updateAirportRequest, airport);
            await _airportRepository.Update(airport);
        }

        public async Task ChangeAirportsStatus(string id)
        {
            var airport = await _airportRepository.GetById(id);
            var currentStatus = airport.Status;
            airport.Status = !currentStatus;
            await _airportRepository.Update(airport);
        }


    }
}

