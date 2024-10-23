using AutoMapper;
using BusinessObjects.ResponseModels.User;
using Repository.Repositories.UserRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services.UserServices
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public UserService(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<List<UserInfoResponseModel>> GetAllUserExceptAdmin()
        {
            var list = await _userRepository.GetAllUserExceptAdmin();
            return _mapper.Map<List<UserInfoResponseModel>>(list);
        }
    }
}
