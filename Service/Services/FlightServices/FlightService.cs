using AutoMapper;
using BusinessObjects.Models;
using BusinessObjects.RequestModels.Flight;
using BusinessObjects.ResponseModels.Flight;
using ExcelDataReader;
using FFilms.Application.Shared.Response;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Repository.Enums;
using Repository.Repositories.AirplaneRepositories;
using Repository.Repositories.AirporRepositories;
using Repository.Repositories.FlightRepositories;
using Repository.Repositories.SeatClassRepositories;
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
        private readonly IAirportRepository _airportRepository;
        private readonly ISeatClassRepository _seatClassRepository;
        private readonly IAirplaneRepository _airplaneRepository;

        public FlightService(IFlightRepository flightRepository, IMapper mapper,
                             IAirportRepository airportRepository, ISeatClassRepository seatClassRepository,
                             IAirplaneRepository airplaneRepository)
        {
            _flightRepository = flightRepository;
            _mapper = mapper;
            _airportRepository = airportRepository;
            _seatClassRepository = seatClassRepository;
            _airplaneRepository = airplaneRepository;
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
            var flights = await _flightRepository.GetFlightsByFilter(from, to, checkin, checkout);

            var flightResponseModels = _mapper.Map<List<FlightResponseModel>>(flights);

            return flightResponseModels;
        }

        public async Task<Result<Flight>> UploadFile(IFormFile file)
        {
            try
            {
                Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);

                if (file == null || file.Length == 0)
                {
                    return new Result<Flight>
                    {
                        Success = false,
                        Message = "Not found"
                    };
                }

                var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "uploads");
                if (!Directory.Exists(uploadsFolder))
                {
                    Directory.CreateDirectory(uploadsFolder);
                }

                var filePath = Path.Combine(uploadsFolder, file.FileName);
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }

                using (var stream = System.IO.File.Open(filePath, FileMode.Open, FileAccess.Read))
                {
                    using (var reader = ExcelReaderFactory.CreateReader(stream))
                    {
                        bool isHeaderSkipped = false;

                        var flights = new List<Flight>();

                        do
                        {
                            while (reader.Read())
                            {
                                if (!isHeaderSkipped)
                                {
                                    isHeaderSkipped = true; // Skip the header row
                                    continue;
                                }

                                var flight = new Flight
                                {
                                    Id = Guid.NewGuid().ToString(),
                                    FlightNumber = reader.GetValue(0).ToString(),
                                    AirplaneId = await GetAirplaneIdByCodeAsync(reader.GetValue(1).ToString()),
                                    DepartureTime = DateTime.Parse(reader.GetValue(2).ToString()),
                                    Duration = int.Parse(reader.GetValue(3).ToString()),
                                    ArrivalTime = DateTime.Parse(reader.GetValue(2).ToString()).AddMinutes(int.Parse(reader.GetValue(3).ToString())),
                                    From = await GetAirportIdByName(reader.GetValue(4).ToString()),
                                    To = await GetAirportIdByName(reader.GetValue(5).ToString()),
                                    Status = FlightStatusEnums.Schedule.ToString()
                                };

                                // Create seat class prices
                                var ticketClasses = new List<TicketClass>
                                {
                                new TicketClass
                                {
                                    Id = Guid.NewGuid().ToString(),
                                    SeatClassId = await GetSeatClassIdByName("Economy"),
                                    Price = decimal.Parse(reader.GetValue(6).ToString()),
                                    Status = "Available"
                                },
                                new TicketClass
                                {
                                    Id = Guid.NewGuid().ToString(),
                                    SeatClassId = await GetSeatClassIdByName("Business"),
                                    Price = decimal.Parse(reader.GetValue(7).ToString()),
                                    Status = "Available"
                                },
                                new TicketClass
                                {
                                    Id = Guid.NewGuid().ToString(),
                                    SeatClassId = await GetSeatClassIdByName("FirstClass"),
                                    Price = decimal.Parse(reader.GetValue(8).ToString()),
                                    Status = "Available"
                                }
                            };

                                flight.TicketClasses = ticketClasses;

                                flights.Add(flight);
                            }
                        } while (reader.NextResult());

                        // Save all flights with their seat classes to the database
                        await _flightRepository.InsertRange(flights);

                    }
                }

                return new Result<Flight>
                {
                    Success = true,
                    Message = "Upload Successfull"
                };
            }
            catch (Exception ex)
            {
                return new Result<Flight>
                {
                    Success = false,
                    Message = ex.Message
                };
            }
        }


        // lấy thông tin của từng cái name ở flight
        private async Task<string> GetAirplaneIdByCodeAsync(string airplaneCode)
        {
            // Gọi phương thức bất đồng bộ để lấy máy bay
            var airplane = await _airplaneRepository.GetAirplaneByCodeAsync(airplaneCode);

            // Kiểm tra nếu máy bay không tồn tại
            if (airplane == null)
            {
                throw new Exception($"Airplane with code '{airplaneCode}' not found");
            }

            // Trả về ID của máy bay
            return airplane.Id.ToString();
        }

        private async Task<string> GetAirportIdByName(string airportName)
        {
            var airport = await _airportRepository.GetAirportByCodeAsync(airportName);
            //var airport = _context.Airports.FirstOrDefault(a => a.Name == airportName);
            if (airport == null)
            {
                throw new Exception($"Airport with name {airportName} not found");
            }
            return airport.Id.ToString();
        }

        private async Task<string> GetSeatClassIdByName(string seatClassName)
        {
            var seatClass = await _seatClassRepository.GetSeatClassBySeatClassName(seatClassName);
            if (seatClass == null)
            {
                throw new Exception($"Seat class with name {seatClassName} not found");
            }
            return seatClass.Id.ToString();
        }
    }
}

