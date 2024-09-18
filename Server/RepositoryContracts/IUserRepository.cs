using System.Collections;
using Entities;

namespace RepositoryContracts;

public interface IUserRepository
{
    Task<User> AddAsync(User user);
    Task UpdateAsync(User user);
    Task DeleteAsync(int id);
    Task AddUserAsync(User user);
    IQueryable<User> GetMany();
    Task<IEnumerable<User>> GetUsersAsync();
}