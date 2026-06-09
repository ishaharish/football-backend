using football_backend.DTOs.Auth;
using football_backend.Models;

namespace football_backend.Services
{
    public interface IAuthService
    {
        Task<User> RegisterAsync(RegisterDto dto);
        Task<string> LoginAsync(LoginDto dto);
    }
}
