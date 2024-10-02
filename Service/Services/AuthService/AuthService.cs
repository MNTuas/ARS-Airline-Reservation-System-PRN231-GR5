using BusinessObjects.Models;
using BusinessObjects.RequestModels;
using FFilms.Application.Shared.Response;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Repository.Repositories.AuthRepositories;
using Repository.Repositories.RankRepositories;
using Service.Enums;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using static Org.BouncyCastle.Asn1.Cmp.Challenge;

namespace Service.Services.AuthService
{
    public class AuthService : IAuthService
    {
        private readonly IAuthRepository _authRepository;
        private readonly IRankRepository _rankRepository;
        private readonly IConfiguration _configuration;
        public AuthService(IAuthRepository authRepository, IConfiguration configuration, IRankRepository rankRepository)
        {
            _authRepository = authRepository;
            _rankRepository = rankRepository;
            _configuration = configuration;
        }
        public async Task<Result<User>> RegisterAsync(RegisterRequest request)
        {
            try
            {
                var existingUserEmail = await _authRepository.GetSingle(x => x.Email == request.Email);
                if (existingUserEmail != null)
                {
                    return new Result<User>
                    {
                        Success = false,
                        Message = "Email is already used!!!",
                    };
                }
                if (request.Password != request.ConfirmPassword)
                {
                    return new Result<User>
                    {
                        Success = false,
                        Message = "Confirm password does not match with password!!!",
                    };
                }
                var existingUserPhoneNumber = await _authRepository.GetSingle(x => x.PhoneNumber == request.PhoneNumber);
                if (existingUserPhoneNumber != null)
                {
                    return new Result<User>
                    {
                        Success = false,
                        Message = "Phone number is already used!!!",
                    };
                }
                var existingUserAvatar = await _authRepository.GetSingle(x => x.Avatar == request.Avatar);
                if (existingUserAvatar != null)
                {
                    return new Result<User>
                    {
                        Success = false,
                        Message = "Avatar is already used!!!",
                    };
                }
                var user = new User()
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = request.Name,
                    Email = request.Email,
                    PhoneNumber = request.PhoneNumber,
                    Address = request.Address,
                    Avatar = request.Avatar,
                    Password = BCrypt.Net.BCrypt.HashPassword(request.Password),
                    Point = 0,
                    RankId = "default-rank-id",
                    Role = UserRolesEnums.User.ToString(),
                    Status = UserStatusEnums.Active.ToString()
                };
                await _authRepository.Insert(user);

                return new Result<User>
                {
                    Success = true,
                    Message = "Create successful!",
                    Data = user
                };
            }
            catch (Exception)
            {
                return new Result<User>
                {
                    Success = false,
                    Message = "Something wrong!!!",
                };
            }
        }

        public async Task<Result<User>> LoginAsync(LoginRequest request)
        {
            try
            {
                var user = await _authRepository.GetSingle(x => x.Email == request.Email);
                if (user == null)
                {
                    return new Result<User>
                    {
                        Success = false,
                        Message = "Wrong email!!!",
                    };
                }
                if (!BCrypt.Net.BCrypt.Verify(request.Password, user.Password))
                {
                    return new Result<User>
                    {
                        Success = false,
                        Message = "Wrong password!!!",
                    };
                }
                var authClaims = new List<Claim>
                    {
                        new Claim(ClaimsIdentity.DefaultRoleClaimType, user.Role),
                        new Claim(ClaimTypes.Email, user.Email),
                    };
                var token = GenerateJwtToken(authClaims);
                return new Result<User>
                {
                    Success = true,
                    Message = new JwtSecurityTokenHandler().WriteToken(token),
                    Data = user
                };
            }
            catch (Exception ex)
            {
                return new Result<User>
                {
                    Success = false,
                    Message = $"Something went wrong: {ex.Message}",
                };
            }
        }

        private JwtSecurityToken GenerateJwtToken(IEnumerable<Claim> authClaims)
        {
            var authenKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Secret"]));

            return new JwtSecurityToken(
                issuer: _configuration["Jwt:ValidIssuer"],
                audience: _configuration["Jwt:ValidAudience"],
                expires: DateTime.Now.AddMinutes(20),
                claims: authClaims,
                signingCredentials: new SigningCredentials(authenKey, SecurityAlgorithms.HmacSha512Signature));
        }
    }
}
