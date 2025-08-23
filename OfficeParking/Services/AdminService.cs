using Microsoft.AspNetCore.Identity;
using OfficeParkingSystem.Models.Domain;
using OfficeParkingSystem.Models.DTO;
using OfficeParkingSystem.Repositories;

namespace OfficeParkingSystem.Services
{
    public class AdminService : IAdminService
    {
        private readonly IUserRepository _userRepository;
        private readonly UserManager<User> _userManager;

        public AdminService(IUserRepository userRepository, UserManager<User> userManager)
        {
            _userRepository = userRepository;
            _userManager = userManager;
        }

        public async Task<List<UserDTO>> GetAllUsersAsync(string currentAdminId)
        {
            var usersFromDb = await _userRepository.GetAllAsync();

            var filteredUsers = usersFromDb.Where(user => user.Id != currentAdminId);

            var userDtos = new List<UserDTO>();
            foreach (var user in filteredUsers)
            {
                var roles = await _userManager.GetRolesAsync(user);
                userDtos.Add(new UserDTO
                {
                    Id = user.Id,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Email = user.Email!,
                    PhoneNo = user.PhoneNo,
                    Vehicles = user.Vehicles?.Select(v => new VehicleDTO
                    {
                        VehicleId = v.VehicleId,
                        RegNo = v.RegNo,
                        Make = v.Make,
                        Model = v.Model,
                        Year = v.Year,
                        Colour = v.Colour,
                        UserId = v.UserId
                    }).ToList()
                });
            }
            return userDtos;
        }

        public async Task<bool> DeleteUserAsync(string userId)
        {
            var user = await _userRepository.GetByIdAsync(userId);
            if (user == null)
            {
                return false;
            }

            await _userRepository.DeleteAsync(userId);
            return true;
        }

        public async Task<bool> ResetUserPasswordAsync(string userId, string newPassword)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return false;
            }

            var token = await _userManager.GeneratePasswordResetTokenAsync(user);

            var result = await _userManager.ResetPasswordAsync(user, token, newPassword);

            return result.Succeeded;
        }
    }
}