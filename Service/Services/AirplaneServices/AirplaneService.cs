using AutoMapper;
using BusinessObjects.Models;
using BusinessObjects.RequestModels.Airplane;
using BusinessObjects.ResponseModels.Airplane;
using BusinessObjects.ResponseModels.Flight;
using Repository.Repositories.AirlineRepositories;
using Repository.Repositories.AirplaneRepositories;
using Repository.Repositories.AirporRepositories;
using Repository.Repositories.FlightRepositories;

namespace Service.Services.AirplaneServices
{
    public class AirplaneService : IAirplaneService
    {
        private readonly IAirplaneRepository _airplaneRepository;
        private readonly IAirlineRepository _airlineRepository;
        private readonly IFlightRepository _flightRepository;
        private readonly IAirportRepository _airportRepository;
        private readonly IMapper _mapper;

        public AirplaneService(IAirplaneRepository airplaneRepository, IMapper mapper, IAirlineRepository airlineRepository,IFlightRepository flightRepository,IAirportRepository airportRepository)
        {
            _airplaneRepository = airplaneRepository;
            _mapper = mapper;
            _airlineRepository = airlineRepository;
            _flightRepository = flightRepository;
            _airportRepository = airportRepository;
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
            var flights = await _flightRepository.Get(x => x.AirplaneId == id);

            var airplaneModel = _mapper.Map<AirplaneResponseModel>(airplane);
            airplaneModel.Flights = new List<FlightResponseModel>();

            foreach (var flight in flights)
            {
                var flightModel = _mapper.Map<FlightResponseModel>(flight);

                var fromAirport = await _airportRepository.GetById(flight.From);
                var toAirport = await _airportRepository.GetById(flight.To);
                var airline = await _airlineRepository.GetById(airplane.AirlinesId);
                flightModel.FromName = fromAirport?.Name;
                flightModel.ToName = toAirport?.Name;
                flightModel.Airlines = airline.Name;
                flightModel.AirplaneCode = airplane.CodeNumber;
                airplaneModel.Flights.Add(flightModel);
            }

            return airplaneModel;
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
            //var codeAirline = await _airlineRepository.GetById(airplane.AirlinesId); 
            //string updateCodeNumber = codeAirline.Code  ;
            _mapper.Map(requestModel, airplane);
            //airplane.CodeNumber = updateCodeNumber + airplane.CodeNumber;

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
