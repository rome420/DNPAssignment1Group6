using Entities;
using System.Threading.Tasks;

namespace RepositoryContracts
{
    public interface IUserRepository
    {
        Task<User> AddAsync(User user);
        Task UpdateAsync(User user);
        Task DeleteAsync(int id);
        Task<User> GetSingleAsync(int id);
        Task<User> GetByIdAsync(int id);
        IQueryable<User> GetMany();
        Task<IEnumerable<User>> GetUsersAsync();
        Task<bool> UserExistsAsync(string username);
        Task<User> GetUserByUsernameAsync(string username); // Add this method
    }
}