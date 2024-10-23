using AutoMapper;
using BusinessObjects.Models;
using BusinessObjects.RequestModels.Flight;
using BusinessObjects.ResponseModels.Flight;
using Repository.Repositories.FlightRepositories;
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

        public async Task UpdateFlight(string flightId, UpdateFlightRequest request)
        {
            var existingFlight = await _flightRepository.GetFlightById(flightId);
            if (existingFlight == null)
            {
                throw new Exception($"Flight with ID {flightId} not found.");
            }

            _mapper.Map(request, existingFlight);

            existingFlight.ArrivalTime = existingFlight.DepartureTime.AddMinutes(request.Duration);
            foreach (var price in request.TicketClassPrices)
            {
                foreach (var item in existingFlight.TicketClasses)
                {
                    if (price.Id.Equals(item.Id))
                    {
                        _mapper.Map(price, item);
                    }
                }
            }

            await _flightRepository.Update(existingFlight);
        }


        public async Task<FlightResponseModel> GetFlightById(string id)
        {
            var flight = await _flightRepository.GetFlightById(id);
            flight.TicketClasses = flight.TicketClasses.OrderBy(t => t.Price).ToList();
            return _mapper.Map<FlightResponseModel>(flight);
        }

        public async Task<List<FlightResponseModel>> GetFlightByFilter(string from, string to, DateTime checkin, DateTime? checkout)
        {
            // Lấy danh sách chuyến bay theo bộ lọc
            var flights = await _flightRepository.GetFlightsByFilter(from, to, checkin, checkout);

            // Sử dụng AutoMapper để chuyển đổi từ danh sách Flight sang FlightResponseModel
            var flightResponseModels = _mapper.Map<List<FlightResponseModel>>(flights);

            return flightResponseModels;
        }


    }
}

