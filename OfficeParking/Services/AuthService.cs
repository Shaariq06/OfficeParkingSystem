using Microsoft.AspNetCore.Identity;
using OfficeParkingSystem.Models.Domain;
using OfficeParkingSystem.Models.DTO;

namespace OfficeParkingSystem.Services
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;

        public AuthService(UserManager<User> userManager, SignInManager<User> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public async Task<IdentityResult> RegisterAsync(RegisterRequestDTO registerRequestDTO)
        {
            var user = new User
            {
                UserName = registerRequestDTO.Email,
                Email = registerRequestDTO.Email,
                FirstName = registerRequestDTO.FirstName,
                LastName = registerRequestDTO.LastName,
                PhoneNo = registerRequestDTO.PhoneNo
            };

            var result = await _userManager.CreateAsync(user, registerRequestDTO.Password);
            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(user, "Employee");
            }
            return result;
        }

        public async Task<SignInResult> LoginAsync(LoginRequestDTO loginRequestDTO)
        {
            return await _signInManager.PasswordSignInAsync(
                loginRequestDTO.Email,
                loginRequestDTO.Password,
                isPersistent: false,
                lockoutOnFailure: false);
        }

        public async Task LogoutAsync()
        {
            await _signInManager.SignOutAsync();
        }
    }
}

