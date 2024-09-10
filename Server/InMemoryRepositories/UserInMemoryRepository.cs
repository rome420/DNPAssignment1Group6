using Entities;
using RepositoryContracts;
namespace InMemoryRepositories;

public class UserInMemoryRepository : IUserRepository
{
    private readonly List<User> _users = new List<User>();

    public async Task<User> AddAsync(User user)
    {
        if (user == null)
        {
            throw new ArgumentNullException(nameof(user));
        }

        user.UserId = _users.Any() 
            ? _users.Max(u => u.UserId) + 1
            : 1;
        _users.Add(user);
        return await Task.FromResult(user);
    }

    public async Task UpdateAsync(User user)
    {
        if (user == null)
        {
            throw new ArgumentNullException(nameof(user));
        }

        var existingUser = _users.SingleOrDefault(u => u.UserId == user.UserId);
        if (existingUser == null)
        {
            throw new InvalidOperationException($"User with ID '{user.UserId}' not found.");
        }

        _users.Remove(existingUser);
        _users.Add(user);

        await Task.CompletedTask;
    }

    public async Task DeleteAsync(int id)
    {
        var userToRemove = _users.SingleOrDefault(u => u.UserId == id);
        if (userToRemove == null)
        {
            throw new InvalidOperationException($"User with ID '{id}' not found.");
        }

        _users.Remove(userToRemove);
        await Task.CompletedTask;
    }

    public async Task<User> GetSingleAsync(int id)
    {
        var user = _users.SingleOrDefault(u => u.UserId == id);
        if (user == null)
        {
            throw new InvalidOperationException($"User with ID '{id}' not found.");
        }

        return await Task.FromResult(user);
    }

    public IQueryable<User> GetMany()
    {
        return _users.AsQueryable();
    }
}
