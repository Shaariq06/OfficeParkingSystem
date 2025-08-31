using OfficeParkingSystem.Models.DTO;

namespace OfficeParkingSystem.Services
{
    public interface IParkingService
    {
        Task<List<ParkingSpotDTO>> GetAllSpotsAsync();
        Task<(bool Success, string Message)> ToggleSpotAsync(int spotId, string userId, int? vehicleId);
    }
}