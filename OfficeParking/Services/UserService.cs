using Microsoft.AspNetCore.Identity;
using OfficeParkingSystem.Models.Domain;
using OfficeParkingSystem.Models.DTO;
using OfficeParkingSystem.Repositories;

namespace OfficeParkingSystem.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly UserManager<User> _userManager;

        public UserService(IUserRepository userRepository, UserManager<User> userManager)
        {
            _userRepository = userRepository;
            _userManager = userManager;
        }

        public async Task<bool> UpdatePhoneNumberAsync(string userId, UpdatePhoneRequestDTO dto)
        {
            var user = await _userRepository.GetByIdAsync(userId);

            if (user == null)
            {
                return false;
            }

            user.PhoneNo = dto.NewPhoneNo;
            await _userRepository.UpdateAsync(user);

            return true;
        }

        public async Task<UserProfileDTO> GetUserProfileAsync(string userId)
        {
            var user = await _userRepository.GetByIdAsync(userId);

            return new UserProfileDTO
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email!,
                PhoneNo = user.PhoneNo
            };
        }

        public async Task<string> GetUserRoleAsync(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                throw new Exception("User not found");
            }
            var roles = await _userManager.GetRolesAsync(user);
            return roles.FirstOrDefault() ?? string.Empty;
        }
    }
}