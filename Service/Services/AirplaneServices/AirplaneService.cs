using AutoMapper;
using BusinessObjects.Models;
using BusinessObjects.RequestModels.Airplane;
using BusinessObjects.ResponseModels.Airplane;
using Repository.Repositories.AirlineRepositories;
using Repository.Repositories.AirplaneRepositories;

namespace Service.Services.AirplaneServices
{
    public class AirplaneService : IAirplaneService
    {
        private readonly IAirplaneRepository _airplaneRepository;
        private readonly IAirlineRepository _airlineRepository;
        private readonly IMapper _mapper;

        public AirplaneService(IAirplaneRepository airplaneRepository, IMapper mapper, IAirlineRepository airlineRepository)
        {
            _airplaneRepository = airplaneRepository;
            _mapper = mapper;
            _airlineRepository = airlineRepository;
        }

        public async Task AddAirplane(AddAirplaneRequest model)
        {
            var airlines = await _airlineRepository.GetById(model.AirlinesId);
            var code = airlines.Code;
            Airplane newAirplane = _mapper.Map<Airplane>(model);
            newAirplane.AirplaneSeats = model.AirplaneSeatRequest
                .Select(_mapper.Map<AirplaneSeat>)
                .ToList();
            newAirplane.CodeNumber = code + model.CodeNumber;
            await _airplaneRepository.Insert(newAirplane);
        }

        public async Task<AirplaneResponseModel> GetAirplane(string id)
        {
            var airplane = await _airplaneRepository.GetAirplane(id);
            return _mapper.Map<AirplaneResponseModel>(airplane);
        }

        public async Task<List<AirplaneResponseModel>> GetAllAirplane()
        {
            var result = await _airplaneRepository.GetAllAirplaneAsync();
            return _mapper.Map<List<AirplaneResponseModel>>(result);
        }

        public async Task UpdateAirplaneAsync(string airplaneId, UpdateAirplaneRequest requestModel)
        {
            var airplane = await _airplaneRepository.GetAirplane(airplaneId);
            if (airplane == null)
            {
                throw new Exception("Airplane not found.");
            }

            _mapper.Map(requestModel, airplane);

            if (requestModel.AirplaneSeatRequest != null)
            {
                var requestSeatIds = requestModel.AirplaneSeatRequest.Select(seat => seat.SeatClassId).ToList();

                var seatsToRemove = airplane.AirplaneSeats
                    .Where(seat => !requestSeatIds.Contains(seat.SeatClassId))
                    .ToList();

                foreach (var seatToRemove in seatsToRemove)
                {
                    airplane.AirplaneSeats.Remove(seatToRemove);
                }

                foreach (var seatRequest in requestModel.AirplaneSeatRequest)
                {
                    var existingSeat = airplane.AirplaneSeats
                        .FirstOrDefault(seat => seat.SeatClassId == seatRequest.SeatClassId);

                    if (existingSeat != null)
                    {
                        _mapper.Map(seatRequest, existingSeat);
                    }
                    else
                    {
                        var newSeat = _mapper.Map<AirplaneSeat>(seatRequest);
                        newSeat.AirplaneId = airplane.Id;
                        newSeat.Id = Guid.NewGuid().ToString();
                        airplane.AirplaneSeats.Add(newSeat);
                    }
                }
            }

            await _airplaneRepository.Update(airplane);
        }

        public async Task SoftRemoveAirplane(string id)
        {
            var airplane = await _airplaneRepository.GetAirplane(id);
            var currentStatus = airplane.Status;
            airplane.Status = !currentStatus;
            await _airplaneRepository.Update(airplane);
        }

    }
}
