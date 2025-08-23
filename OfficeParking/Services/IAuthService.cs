using Microsoft.AspNetCore.Identity;
using OfficeParkingSystem.Models.DTO;

namespace OfficeParkingSystem.Services
{
    public interface IAuthService
    {
        Task<IdentityResult> RegisterAsync(RegisterRequestDTO registerRequestDTO);
        Task<SignInResult> LoginAsync(LoginRequestDTO loginRequestDTO);
        Task LogoutAsync();
    }
}