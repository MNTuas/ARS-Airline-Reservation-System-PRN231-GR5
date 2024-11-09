using AutoMapper;
using BusinessObjects.Models;
using BusinessObjects.RequestModels.Passenger;
using BusinessObjects.ResponseModels.Passenger;
using FFilms.Application.Shared.Response;
using Microsoft.AspNetCore.Http;
using Repository.Enums;
using Repository.Repositories.PassengerRepositories;
using Repository.Repositories.UserRepositories;
using Service.Helper;

namespace Service.Services.PassengerServices
{
    public class PassengerService : IPassengerService
    {
        private readonly IPassengerRepository _PassengerRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IMapper _mapper;
        private readonly IUserRepository _userRepository;

        public PassengerService(IPassengerRepository PassengerRepository, IHttpContextAccessor httpContextAccessor,
                                IMapper mapper, IUserRepository userRepository)
        {
            _PassengerRepository = PassengerRepository;
            _httpContextAccessor = httpContextAccessor;
            _mapper = mapper;
            _userRepository = userRepository;
        }

        public async Task<Result<Passenger>> addPassenger(CreatePassengerRequest createPassengerRequest)
        {
            try
            {
                var idclaim = _httpContextAccessor.HttpContext.User.FindFirst(MySetting.CLAIM_USERID);
                var userid = idclaim.Value;

                // Tính toán tuổi hiện tại
                var currentDate = DateOnly.FromDateTime(DateTime.UtcNow);
                var age = currentDate.Year - createPassengerRequest.Dob.Year;

                // Nếu ngày sinh chưa đến ngày hiện tại trong năm nay, giảm tuổi đi 1
                if (currentDate < createPassengerRequest.Dob.AddYears(age))
                {
                    age--;
                }

                // Thiết lập loại hành khách dựa trên độ tuổi
                PassengerTypeEnum passengerType;
                if (age < 2)
                {
                    passengerType = PassengerTypeEnum.Infant;
                }
                else if (age >= 2 && age < 12)
                {
                    passengerType = PassengerTypeEnum.Child;
                }
                else
                {
                    passengerType = PassengerTypeEnum.Adult;
                }

                var newPassenger = new Passenger
                {
                    Id = Guid.NewGuid().ToString(),
                    Country = createPassengerRequest.Country,
                    Dob = createPassengerRequest.Dob,
                    FirstName = createPassengerRequest.FirstName,
                    LastName = createPassengerRequest.LastName,
                    Gender = createPassengerRequest.Gender,
                    UserId = userid,
                    Type = passengerType.ToString() // Gán giá trị loại hành khách vào thuộc tính Type
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

        public async Task<List<PassengerResposeModel>> GetPassengerByLogin()
        {
            var idclaim = _httpContextAccessor.HttpContext.User.FindFirst(MySetting.CLAIM_USERID);
            var userid = idclaim?.Value;

            if (string.IsNullOrEmpty(userid))
            {
                throw new Exception("User ID not found in claims.");
            }

            var passengers = await _PassengerRepository.GetByLogin(userid);
            return _mapper.Map<List<PassengerResposeModel>>(passengers);
        }


        public async Task<PassengerResposeModel> GetDetailsPassengerInfo(string id)
        {
            var passenger = await _PassengerRepository.GetById(id);
            return _mapper.Map<PassengerResposeModel>(passenger);
        }

        public async Task UpdatePassenger(string id, UpdatePassengerRequest request)
        {
            var passenger = await _PassengerRepository.GetById(id);
            var currentDate = DateOnly.FromDateTime(DateTime.UtcNow);
            var age = currentDate.Year - request.Dob.Year;
            if (currentDate < request.Dob.AddYears(age))
            {
                age--;
            }
            PassengerTypeEnum passengerType;
            if (age < 2)
            {
                passengerType = PassengerTypeEnum.Infant;
            }
            else if (age >= 2 && age < 12)
            {
                passengerType = PassengerTypeEnum.Child;
            }
            else
            {
                passengerType = PassengerTypeEnum.Adult;
            }
            passenger.Type = passengerType.ToString();
            _mapper.Map(request, passenger);
            await _PassengerRepository.Update(passenger);
        }

        public async Task DeletePassenger(string id)
        {
            var passenger = await _PassengerRepository.GetById(id);
            if(passenger == null)
            {
                throw new Exception("Passenger not found");
            }
            await _PassengerRepository.Delete(passenger);
        }
    }
}
