using OfficeParkingSystem.Models.DTO;
using OfficeParkingSystem.Repositories;

namespace OfficeParkingSystem.Services
{
    public class ParkingService : IParkingService
    {
        private readonly IParkingSpotRepository _parkingSpotRepository;

        public ParkingService(IParkingSpotRepository parkingSpotRepository)
        {
            _parkingSpotRepository = parkingSpotRepository;
        }

        public async Task<List<ParkingSpotDTO>> GetAllSpotsAsync()
        {
            var spots = await _parkingSpotRepository.GetAllAsync(); // Ensure this includes Vehicle details

            return spots.Select(s => new ParkingSpotDTO
            {
                Id = s.Id,
                SpotNumber = s.SpotNumber,
                IsOccupied = s.IsOccupied,
                OccupiedBy = s.OccupiedByUser != null ? $"{s.OccupiedByUser.FirstName} {s.OccupiedByUser.LastName.FirstOrDefault()}." : null,
                VehicleInfo = s.OccupiedByVehicle != null ? $"{s.OccupiedByVehicle.Make} {s.OccupiedByVehicle.Model} ({s.OccupiedByVehicle.RegNo})" : null
            }).ToList();
        }

        public async Task<(bool Success, string Message)> ToggleSpotAsync(int spotId, string userId, int? vehicleId)
        {
            var spotToToggle = await _parkingSpotRepository.GetByIdAsync(spotId);
            if (spotToToggle == null)
            {
                return (false, "Parking spot not found.");
            }

            if (spotToToggle.IsOccupied && spotToToggle.OccupiedByUserId == userId)
            {
                spotToToggle.IsOccupied = false;
                spotToToggle.OccupiedByUserId = null;
                spotToToggle.OccupiedByVehicleId = null;
                await _parkingSpotRepository.UpdateAsync(spotToToggle);
                return (true, "Spot vacated successfully.");
            }

            if (spotToToggle.IsOccupied)
            {
                return (false, "This spot is already taken by another user.");
            }

            var existingSpot = await _parkingSpotRepository.FindByOccupantIdAsync(userId);
            if (existingSpot != null)
            {
                return (false, $"You are already parked in spot {existingSpot.SpotNumber}. Please vacate it first.");
            }

            spotToToggle.IsOccupied = true;
            spotToToggle.OccupiedByUserId = userId;
            spotToToggle.OccupiedByVehicleId = vehicleId;
            await _parkingSpotRepository.UpdateAsync(spotToToggle);
            return (true, "Spot occupied successfully.");
        }
    }
}