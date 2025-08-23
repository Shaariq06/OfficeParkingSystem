using Microsoft.EntityFrameworkCore;
using OfficeParkingSystem.Data;
using OfficeParkingSystem.Models.Domain;

namespace OfficeParkingSystem.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _context;

        public UserRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<User> GetByIdAsync(string id)
        {
            var user = await _context.Users.FindAsync(id);
            return user ?? throw new KeyNotFoundException($"User with ID '{id}' not found.");
        }

        public async Task UpdateAsync(User user)
        {
            _context.Users.Update(user);
            await _context.SaveChangesAsync();
        }

        public async Task<List<User>> GetAllAsync()
        {
            return await _context.Users
            .Include(u => u.Vehicles)
            .ToListAsync();
        }

        public async Task DeleteAsync(string id)
        {
            var user = await GetByIdAsync(id);
            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
        }
    }
}