using OfficeParkingSystem.Models.Domain;

namespace OfficeParkingSystem.Repositories
{
    public interface IParkingSpotRepository
    {
        Task<List<ParkingSpot>> GetAllAsync();
        Task<ParkingSpot?> GetByIdAsync(int spotId);
        Task UpdateAsync(ParkingSpot spot);
        Task<ParkingSpot?> FindByOccupantIdAsync(string userId);
    }
}