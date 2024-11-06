using AutoMapper;
using BusinessObjects.Models;
using BusinessObjects.RequestModels.Flight;
using BusinessObjects.ResponseModels.Flight;
using ExcelDataReader;
using FFilms.Application.Shared.Response;
using Microsoft.AspNetCore.Http;
using Repository.Enums;
using Repository.Repositories.AirlineRepositories;
using Repository.Repositories.AirplaneRepositories;
using Repository.Repositories.AirporRepositories;
using Repository.Repositories.FlightRepositories;
using Repository.Repositories.SeatClassRepositories;
using System.Text;

namespace Service.Services.FlightServices
{
    public class FlightService : IFlightService
    {
        private readonly IFlightRepository _flightRepository;
        private readonly IMapper _mapper;
        private readonly IAirportRepository _airportRepository;
        private readonly ISeatClassRepository _seatClassRepository;
        private readonly IAirplaneRepository _airplaneRepository;
        private readonly IAirlineRepository _airlineRepository;

        public FlightService(IFlightRepository flightRepository, IMapper mapper,
                             IAirportRepository airportRepository, ISeatClassRepository seatClassRepository,
                             IAirplaneRepository airplaneRepository, IAirlineRepository airlineRepository)
        {
            _flightRepository = flightRepository;
            _mapper = mapper;
            _airportRepository = airportRepository;
            _seatClassRepository = seatClassRepository;
            _airplaneRepository = airplaneRepository;
            _airlineRepository = airlineRepository;
        }

        public async Task CreateFlight(CreateFlightRequest request)
        {
            var airline = await _airplaneRepository.GetAirplane(request.AirplaneId);
            var code = _airlineRepository.GetById(airline.AirlinesId);
            Flight newFlight = _mapper.Map<Flight>(request);
            newFlight.FlightNumber = code + request.FlightNumber;
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

        //upload excel flight
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

                var flights = new List<Flight>();
                var existingFlightNumbers = new HashSet<string>(); // Để lưu trữ các chuyến bay đã kiểm tra
                var existingAirplane = new HashSet<string>();
                var flightsPerDay = new Dictionary<string, int>();

                using (var stream = System.IO.File.Open(filePath, FileMode.Open, FileAccess.Read))
                {
                    using (var reader = ExcelReaderFactory.CreateReader(stream))
                    {
                        bool isHeaderSkipped = false;

                        do
                        {
                            while (reader.Read())
                            {
                                if (!isHeaderSkipped)
                                {
                                    isHeaderSkipped = true;
                                    continue;
                                }

                                if (reader.GetValue(0) == null || string.IsNullOrWhiteSpace(reader.GetValue(0).ToString()))
                                {
                                    break;
                                }

                                var flightNumber = reader.GetValue(0).ToString();
                                var departureTime = DateTime.Parse(reader.GetValue(2).ToString());
                                var AirplaneId = await GetAirplaneIdByCodeAsync(reader.GetValue(1).ToString());

                                if (departureTime < DateTime.UtcNow)
                                {
                                    return new Result<Flight>
                                    {
                                        Success = false,
                                        Message = "Departure time is pass over"
                                    };
                                }

                                if (existingFlightNumbers.Contains($"{flightNumber}|{departureTime}"))
                                {
                                    return new Result<Flight>
                                    {
                                        Success = false,
                                        Message = $"Flight Number '{flightNumber}' already exists for the given departure time."
                                    };
                                }

                                var dateKey = $"{AirplaneId}|{departureTime.Date}";

                                if (flightsPerDay.ContainsKey(dateKey))
                                {
                                    flightsPerDay[dateKey]++;
                                }
                                else
                                {
                                    flightsPerDay[dateKey] = 1;
                                }

                                // Kiểm tra nếu số chuyến bay đã vượt quá 2
                                if (flightsPerDay[dateKey] > 2)
                                {
                                    return new Result<Flight>
                                    {
                                        Success = false,
                                        Message = "Airplane already have 2 flight per day"
                                    };
                                }

                                var existingFlight = await _flightRepository.GetFlightByNumber(flightNumber, departureTime);
                                if (existingFlight != null)
                                {
                                    return new Result<Flight>
                                    {
                                        Success = false,
                                        Message = $"Flight Number '{flightNumber}' already exists for the given departure time."
                                    };
                                }

                                var flightInDay = await _flightRepository.CountFlightsForAirplaneOnDate(AirplaneId, departureTime);
                                if (flightInDay > 2)
                                {
                                    return new Result<Flight>
                                    {
                                        Success = false,
                                        Message = "Airplane already have 2 flight per day "
                                    };
                                }



                                var flight = new Flight
                                {
                                    Id = Guid.NewGuid().ToString(),
                                    FlightNumber = flightNumber,
                                    AirplaneId = AirplaneId,
                                    DepartureTime = departureTime,
                                    Duration = ParsePositiveDuration(reader.GetValue(3).ToString()),
                                    ArrivalTime = departureTime.AddMinutes(int.Parse(reader.GetValue(3).ToString())),
                                    From = await GetAirportIdByName(reader.GetValue(4).ToString()),
                                    To = await GetAirportIdByName(reader.GetValue(5).ToString()),
                                    Status = FlightStatusEnums.Scheduled.ToString()
                                };

                                var ticketClasses = new List<TicketClass>
                                {
                                    new TicketClass
                                    {
                                        FlightId = flight.Id,
                                        Id = Guid.NewGuid().ToString(),
                                        SeatClassId = await GetSeatClassIdByName("Economy"),
                                        Price = ParsePositivePrice(reader.GetValue(6).ToString()),
                                        Status = "Available"
                                    },
                                    new TicketClass
                                    {
                                        FlightId = flight.Id,
                                        Id = Guid.NewGuid().ToString(),
                                        SeatClassId = await GetSeatClassIdByName("Business"),
                                        Price = ParsePositivePrice(reader.GetValue(7).ToString()),
                                        Status = "Available"
                                    },
                                    new TicketClass
                                    {
                                        FlightId = flight.Id,
                                        Id = Guid.NewGuid().ToString(),
                                        SeatClassId = await GetSeatClassIdByName("FirstClass"),
                                        Price = ParsePositivePrice(reader.GetValue(8).ToString()),
                                        Status = "Available"
                                    }
                                };

                                flight.TicketClasses = ticketClasses;
                                flights.Add(flight);

                                // Thêm chuyến bay vào bộ nhớ để kiểm tra cho các chuyến bay tiếp theo
                                existingFlightNumbers.Add($"{flightNumber}|{departureTime}");
                                existingAirplane.Add($"{AirplaneId}|{departureTime}");
                            }

                        } while (reader.NextResult());
                    }
                }

                if (flights.Count > 0)
                {
                    await _flightRepository.InsertRange(flights);
                }

                return new Result<Flight>
                {
                    Success = true,
                    Message = "Upload Successful"
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

        private async Task<string> GetAirplaneIdByCodeAsync(string airplaneCode)
        {
            var airplane = await _airplaneRepository.GetAirplaneByCodeAsync(airplaneCode);

            if (airplane == null)
            {
                throw new Exception($"Airplane with code '{airplaneCode}' not found");
            }

            return airplane.Id.ToString();
        }

        private async Task<string> GetAirportIdByName(string airportName)
        {
            var airport = await _airportRepository.GetAirportByCodeAsync(airportName);
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

        private decimal ParsePositivePrice(string priceString)
        {
            if (decimal.TryParse(priceString, out decimal price) && price >= 0)
            {
                return price;
            }
            else
            {
                throw new ArgumentException("Price must be a positive decimal value.");
            }
        }

        private int ParsePositiveDuration(string durationString)
        {
            if (int.TryParse(durationString, out int duration) && duration >= 0)
            {
                return duration;
            }
            else
            {
                throw new ArgumentException("Duration must be a non-negative integer.");
            }
        }

        public async Task<string> AutoUpdateFlightStatus()
        {
            var scheduledFlightList = await _flightRepository.GetAllScheduledFlight();
            var updateList = new List<Flight>();
            foreach (var flight in scheduledFlightList)
            {
                if (DateTime.Now.CompareTo(flight.ArrivalTime) > 0)
                {
                    flight.Status = FlightStatusEnums.Arrived.ToString();
                    updateList.Add(flight);
                }
            }
            await _flightRepository.UpdateRange(updateList);
            return "Flight update successfully!";
        }

    }
}

