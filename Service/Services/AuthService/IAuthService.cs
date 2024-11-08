using BusinessObjects.Models;
using BusinessObjects.RequestModels.Auth;
using FFilms.Application.Shared.Response;

namespace Service.Services.AuthService
{
    public interface IAuthService
    {
        Task<Result<User>> RegisterAsync(RegisterRequest request);
        Task<Result<string>> LoginAsync(LoginRequest request);
        Task<Result<string>> ChangePassword(string userId, ChangePasswordRequest newPassword);
    }
}
