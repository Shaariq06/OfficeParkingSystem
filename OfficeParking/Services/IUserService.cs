using OfficeParkingSystem.Models.DTO;

namespace OfficeParkingSystem.Services
{
    public interface IUserService
    {
        Task<bool> UpdatePhoneNumberAsync(string userId, UpdatePhoneRequestDTO dto);
        Task<UserProfileDTO> GetUserProfileAsync(string userId);
        Task<string> GetUserRoleAsync(string userId);
    }
}