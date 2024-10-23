using BusinessObjects.Models;
using BusinessObjects.RequestModels.Auth;
using FFilms.Application.Shared.Response;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Repository.Enums;
using Repository.Repositories.AuthRepositories;
using Repository.Repositories.RankRepositories;
using Service.Helper;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services.AuthService
{
    public class AuthService : IAuthService
    {
        private readonly IAuthRepository _authRepository;
        private readonly IConfiguration _configuration;
        public AuthService(IAuthRepository authRepository, IConfiguration configuration)
        {
            _authRepository = authRepository;
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

                var rankId = await _rankRepository.GetRankIdByName(RankEnums.Bronze.ToString());

                var user = new User()
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = request.Name,
                    Email = request.Email,
                    Password = BCrypt.Net.BCrypt.HashPassword(request.Password),
                    Point = 0,
                    RankId = rankId,
                    Role = UserRolesEnums.User.ToString(),
                    Status = UserStatusEnums.Active.ToString(),
                };

                await _authRepository.Insert(user);

                return new Result<User>
                {
                    Success = true,
                    Message = "Register successful!",
                    Data = user
                };
            }
            catch (Exception ex)
            {
                return new Result<User>
                {
                    Success = false,
                    Message = ex.Message,
                };
            }
        }

        public async Task<Result<string>> LoginAsync(LoginRequest request)
        {
            try
            {
                var user = await _authRepository.GetSingle(x => x.Email == request.Email);
                if (user == null)
                {
                    return new Result<string>
                    {
                        Success = false,
                        Message = "Wrong email!!!",
                    };
                }
                if (!BCrypt.Net.BCrypt.Verify(request.Password, user.Password))
                {
                    return new Result<string>
                    {
                        Success = false,
                        Message = "Wrong password!!!",
                    };
                }
                var authClaims = new List<Claim>
                    {
                        new Claim(MySetting.CLAIM_USERID, user.Id),
                        new Claim(ClaimTypes.Role, user.Role),
                        new Claim("Email", user.Email),
                        new Claim("UserId", user.Id),
                        new Claim("Username", user.Name),
                    };
                var token = GenerateJwtToken(authClaims);

                return new Result<string>
                {
                    Success = true,
                    Message = "Login successfully",
                    Data = new JwtSecurityTokenHandler().WriteToken(token)
                };
            }
            catch (Exception ex)
            {
                return new Result<string>
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
                expires: DateTime.Now.AddMonths(1),
                claims: authClaims,
                signingCredentials: new SigningCredentials(authenKey, SecurityAlgorithms.HmacSha512Signature));
        }

        public async Task<Result<string>> ChangePassword(string userId, ChangePasswordRequest passwordRequest)
        {
            var user = await _authRepository.GetSingle(i => i.Id == userId);
            if (user == null) {
                return new Result<string>
                {
                    Success = false,
                    Message = "User not found",
                };
            }
            bool isValid = BCrypt.Net.BCrypt.Verify(passwordRequest.oldPassword, user.Password);
            if (isValid)
            {
                user.Password = BCrypt.Net.BCrypt.HashPassword(passwordRequest.newPassword);
                await _authRepository.Update(user);
                return new Result<string>
                {
                    Success = true,
                    Message = "Change password successfully"
                };
            }
            return new Result<string>
            {
                Success = false,
                Message = "Old password wrong"
                
            };
        }
    }
}
