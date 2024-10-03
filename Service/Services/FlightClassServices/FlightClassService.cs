using BusinessObjects.Models;
using BusinessObjects.RequestModels;
using BusinessObjects.ResponseModels;
using FFilms.Application.Shared.Response;
using Repository.Repositories.FlightClassRepositories;
using Repository.Repositories.FlightRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services.FlightClassServices
{
    public class FlightClassService : IFlightClassService
    {
        private readonly IFlightClassRepository _flightClassRepository;
        private readonly IFlightRepository _flightRepository;
        public FlightClassService(IFlightClassRepository flightClassRepository, IFlightRepository flightRepository)
        {
            _flightClassRepository = flightClassRepository;
            _flightRepository = flightRepository;
        }
        public async Task<FlightClass> GetFlightById(string id)
        {
            return await _flightClassRepository.GetById(id);
        }
        public async Task<List<FlightClassResponseModel>> GetAllFlightClassse()
        {
            var result = await _flightClassRepository.GetAllFlightClasses();
            return result.Select(r => new FlightClassResponseModel
            {
                Id = r.Id,
                Class = r.Class,
                FlightId = r.FlightId,
                Quantity = r.Quantity,
                Price = r.Price,
            }).ToList();
        }

        public async Task<Result<FlightClass>> AddFlightClass(FlightClassRequest request)
        {
            try
            {
                var existingClass = await _flightClassRepository.GetSingle(x => x.Class ==  request.Class);
                if(existingClass != null) 
                {
                    return new Result<FlightClass>
                    {
                        Success = false,
                        Message = "Class already existed"
                    };
                }

                var newFlightClass = new FlightClass
                {
                    Id = Guid.NewGuid().ToString(),
                    FlightId = request.FlightId,
                    Class = request.Class,
                    Quantity = request.Quantity,
                    Price = request.Price,
                };
                await _flightClassRepository.Insert(newFlightClass);

                return new Result<FlightClass>
                {
                    Success = true,
                    Message = "Create successful!",
                    Data = newFlightClass
                };
            }
            catch
            {
                return new Result<FlightClass>
                {
                    Success = false,
                    Message = "Something wrong!!!",
                };
            }
        }

        public async Task<Result<FlightClass>> UpdateFlightClass(FlightClassRequest request, string id)
        {
            try
            {
                var existingFlightlass = await _flightClassRepository.GetById(id);
                if(existingFlightlass == null)
                {
                    return new Result<FlightClass>
                    {
                        Success = false,
                        Message = "Flight class does not exist"
                    };
                }

                var existingFlight = await _flightRepository.GetById(id);
                if (existingFlight == null)
                {
                    return new Result<FlightClass>
                    {
                        Success = false,
                        Message = "Flight does not exist"
                    };
                }

                var existingClass = await _flightClassRepository.GetSingle(x => x.Class == request.Class && x.Id != id);
                if (existingClass != null)
                {
                    return new Result<FlightClass>
                    {
                        Success = false,
                        Message = "Class already existed"
                    };
                }

                existingFlightlass.FlightId = request.FlightId;
                existingFlightlass.Class = request.Class;
                existingFlightlass.Quantity = request.Quantity;
                existingFlightlass.Price = request.Price;

                await _flightClassRepository.Update(existingFlightlass);

                return new Result<FlightClass>
                {
                    Success = true,
                    Message = "Update successful!",
                    Data = existingFlightlass
                };
            }
            catch
            {
                return new Result<FlightClass>
                {
                    Success = false,
                    Message = "Something wrong!!!",
                };
            }
        }
    }
}
