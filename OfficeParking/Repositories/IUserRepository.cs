using OfficeParkingSystem.Models.Domain;

namespace OfficeParkingSystem.Repositories
{
    public interface IUserRepository
    {
        Task<User> GetByIdAsync(string id);
        Task UpdateAsync(User user);
        Task<List<User>> GetAllAsync();
        Task DeleteAsync(string id);
    }
}