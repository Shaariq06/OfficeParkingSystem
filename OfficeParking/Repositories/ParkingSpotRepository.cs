using Microsoft.EntityFrameworkCore;
using OfficeParkingSystem.Data;
using OfficeParkingSystem.Models.Domain;

namespace OfficeParkingSystem.Repositories
{
    public class ParkingSpotRepository : IParkingSpotRepository
    {
        private readonly ApplicationDbContext _context;

        public ParkingSpotRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<ParkingSpot>> GetAllAsync()
        {
            return await _context.ParkingSpots
                .Include(s => s.OccupiedByUser)
                .ToListAsync();
        }

        public async Task<ParkingSpot?> GetByIdAsync(int spotId)
        {
            return await _context.ParkingSpots.FindAsync(spotId);
        }

        public async Task UpdateAsync(ParkingSpot spot)
        {
            _context.ParkingSpots.Update(spot);
            await _context.SaveChangesAsync();
        }

        public async Task<ParkingSpot?> FindByOccupantIdAsync(string userId)
        {
            return await _context.ParkingSpots
                .FirstOrDefaultAsync(s => s.OccupiedByUserId == userId);
        }
    }
}