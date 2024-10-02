using BusinessObjects.Models;
using BusinessObjects.RequestModels;
using FFilms.Application.Shared.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services.AuthService
{
    public interface IAuthService
    {
        Task<Result<User>> RegisterAsync(RegisterRequest request);
        Task<Result<string>> LoginAsync(LoginRequest request);
    }
}
