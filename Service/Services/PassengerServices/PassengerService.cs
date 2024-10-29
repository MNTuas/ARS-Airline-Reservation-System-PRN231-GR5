using BusinessObjects.Models;
using BusinessObjects.RequestModels.Passenger;
using FFilms.Application.Shared.Response;
using Microsoft.AspNetCore.Http;
using Repository.Repositories.PassengerRepositories;
using Service.Enums;
using Service.Helper;
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

        public PassengerService(IPassengerRepository PassengerRepository, IHttpContextAccessor httpContextAccessor)
        {
            _PassengerRepository = PassengerRepository;
            _httpContextAccessor = httpContextAccessor;
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
                    Dob = DateOnly.FromDateTime(DateTime.UtcNow),
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
    }
}
