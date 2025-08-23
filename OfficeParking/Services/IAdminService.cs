using OfficeParkingSystem.Models.DTO;

namespace OfficeParkingSystem.Services
{
    public interface IAdminService
    {
        Task<List<UserDTO>> GetAllUsersAsync(string currentAdminId);
        Task<bool> DeleteUserAsync(string userId);
        Task<bool> ResetUserPasswordAsync(string userId, string newPassword);
    }
}