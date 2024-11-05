using AutoMapper;
using BusinessObjects.Models;
using BusinessObjects.RequestModels.Airlines;
using BusinessObjects.ResponseModels.Airlines;
using Repository.Repositories.AirlineRepositories;

namespace Service.Services.AirlineServices
{
    public class AirlineService : IAirlineService
    {
        private readonly IAirlineRepository _airlineRepository;
        private readonly IMapper _mapper;

        public AirlineService(IAirlineRepository airlineRepository, IMapper mapper)
        {
            _airlineRepository = airlineRepository;
            _mapper = mapper;
        }

        public async Task<List<AllAirlinesResponseModel>> GetAllAirlines()
        {
            var list = await _airlineRepository.GetAllAirlines();
            return _mapper.Map<List<AllAirlinesResponseModel>>(list);
        }

        public async Task<AirlinesResponseModel> GetDetailsAirlineInfo(string id)
        {
            var airlines = await _airlineRepository.GetDetailsById(id);
            return _mapper.Map<AirlinesResponseModel>(airlines);
        }

        public async Task AddAirlines(AirlinesCreateModel model)
        {
            Airline newAirline = _mapper.Map<Airline>(model);
            await _airlineRepository.Insert(newAirline);
        }

        public async Task UpdateAirlines(string id, AirlinesUpdateModel model)
        {
            var airline = await _airlineRepository.GetById(id);
            _mapper.Map(model, airline);
            await _airlineRepository.Update(airline);
        }

        public async Task ChangeAirlinesStatus(string id)
        {
            var airline = await _airlineRepository.GetById(id);
            var currentStatus = airline.Status;
            airline.Status = !currentStatus;
            await _airlineRepository.Update(airline);
        }
    }
}
