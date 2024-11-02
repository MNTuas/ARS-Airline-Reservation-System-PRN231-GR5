using AutoMapper;
using BusinessObjects.Models;
using BusinessObjects.RequestModels.Airlines;
using BusinessObjects.RequestModels.Passenger;
using BusinessObjects.ResponseModels.Airlines;
using BusinessObjects.ResponseModels.Airport;
using BusinessObjects.ResponseModels.Passenger;
using BusinessObjects.ResponseModels.User;
using FFilms.Application.Shared.Response;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using Repository.Repositories.PassengerRepositories;
using Repository.Repositories.UserRepositories;
using Service.Enums;
using Service.Helper;
using Service.Ultis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services.PassengerServices
{
    public class PassengerService : IPassengerService
    {
        private readonly IPassengerRepository _PassengerRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IMapper _mapper;

        public PassengerService(IPassengerRepository PassengerRepository, IHttpContextAccessor httpContextAccessor, IMapper mapper)
        {
            _PassengerRepository = PassengerRepository;
            _httpContextAccessor = httpContextAccessor;
            _mapper = mapper;
        }

        public async Task<Result<Passenger>> addPassenger(CreatePassengerRequest createPassengerRequest)
        {
            try
            {

                var idclaim = _httpContextAccessor.HttpContext.User.FindFirst(MySetting.CLAIM_USERID);
                var userid = idclaim.Value;


                var newPassenger = new Passenger
                {
                    Id = Guid.NewGuid().ToString(),
                    Country = createPassengerRequest.Country,
                    Dob = createPassengerRequest.Dob,
                    FirstName = createPassengerRequest.FirstName,
                    LastName = createPassengerRequest.LastName,
                    Gender = createPassengerRequest.Gender,
                    Type = createPassengerRequest.Type,
                    UserId = userid,
                };

                await _PassengerRepository.Insert(newPassenger);
                return new Result<Passenger>
                {
                    Success = true,
                    Data = newPassenger
                };

            }
            catch (Exception ex)
            {
                return new Result<Passenger>
                {
                    Success = false,
                    Message = ex.Message,
                };
            }
        }
        public async Task<List<PassengerResposeModel>> GetAllPassengers()
        {
            var result = await _PassengerRepository.GetAllPassenger();
            return _mapper.Map<List<PassengerResposeModel>>(result);
        }
        public async Task<PassengerResposeModel> GetDetailsPassengerInfo(string id)
        {
            var passenger = await _PassengerRepository.GetById(id);
            return _mapper.Map<PassengerResposeModel>(passenger);
        }
        public async Task UpdatePassenger(string id, UpdatePassengerRequest request)
        {
            var passenger = await _PassengerRepository.GetById(id);
            _mapper.Map(request, passenger);
            await _PassengerRepository.Update(passenger);
        }
        public async Task DeletePassenger(string id)
        {
            var passenger = await _PassengerRepository.GetById(id);
            await _PassengerRepository.Delete(passenger);
        }
    }
}
