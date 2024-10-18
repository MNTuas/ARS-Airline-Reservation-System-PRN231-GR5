using AutoMapper;
using BusinessObjects.Models;
using BusinessObjects.RequestModels.Flight;
using BusinessObjects.ResponseModels.Flight;
using Repository.Repositories.FlightRepositories;
using Service.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services.FlightServices
{
    public class FlightService : IFlightService
    {
        private readonly IFlightRepository _flightRepository;
        private readonly IMapper _mapper;

        public FlightService(IFlightRepository flightRepository, IMapper mapper)
        {
            _flightRepository = flightRepository;
            _mapper = mapper;
        }

        public async Task CreateFlight(CreateFlightRequest request)
        {
            Flight newFlight = _mapper.Map<Flight>(request);
            await _flightRepository.Insert(newFlight);
        }

        public async Task<List<FlightResponseModel>> GetAllFlights()
        {
            var list = await _flightRepository.GetAllFlights();
            return _mapper.Map<List<FlightResponseModel>>(list);
        }

        public async Task UpdateFlight(UpdateFlightRequest request, string id)
        {
            var flight = await _flightRepository.GetFlightById(id);
            _mapper.Map(request, flight);
            await _flightRepository.Update(flight);
        }

        public async Task<FlightResponseModel> GetFlightById(string id)
        {
            var flight = await _flightRepository.GetFlightById(id);
            flight.TicketClasses = flight.TicketClasses.OrderBy(t => t.Price).ToList();
            return _mapper.Map<FlightResponseModel>(flight);
        }


    }
}

