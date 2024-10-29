using Entities;

namespace RepositoryContracts
{
    public interface IUserRepository
    {
        Task<User> AddAsync(User user);
        Task UpdateAsync(User user);
        Task DeleteAsync(int id);
        Task<User> GetSingleAsync(int id); // Ensure this method exists
        Task<User> GetByIdAsync(int id); // Add this method
        IQueryable<User> GetMany();
        Task<IEnumerable<User>> GetUsersAsync();
        Task<bool> UserExistsAsync(string username);
    }
}