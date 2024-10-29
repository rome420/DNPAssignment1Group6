using System.Text.Json;
using Entities;
using RepositoryContracts;

namespace FileRepository
{
    public class UserFileRepository : IUserRepository
    {
        private readonly string filePath = "users.json";

        public UserFileRepository()
        {
            if (!File.Exists(filePath))
            {
                File.WriteAllText(filePath, "[]");
            }
        }

        public async Task<User> AddAsync(User user)
        {
            string usersAsJson = await File.ReadAllTextAsync(filePath);
            List<User> users = JsonSerializer.Deserialize<List<User>>(usersAsJson) ?? new List<User>();

            int maxId = users.Count > 0 ? users.Max(u => u.UserId) : 0;
            user.UserId = maxId + 1;
            users.Add(user);

            usersAsJson = JsonSerializer.Serialize(users);
            await File.WriteAllTextAsync(filePath, usersAsJson);

            return user;
        }

        public Task AddUserAsync(User user)
        {
            return AddAsync(user);  // Reuse AddAsync to handle user addition
        }

        public async Task<IEnumerable<User>> GetUsersAsync()
        {
            string usersAsJson = await File.ReadAllTextAsync(filePath);
            List<User> users = JsonSerializer.Deserialize<List<User>>(usersAsJson) ?? new List<User>();

            return users;
        }

        public async Task UpdateAsync(User user)
        {
            string usersAsJson = await File.ReadAllTextAsync(filePath);
            List<User> users = JsonSerializer.Deserialize<List<User>>(usersAsJson) ?? new List<User>();

            var existingUser = users.SingleOrDefault(u => u.UserId == user.UserId);
            if (existingUser == null)
            {
                throw new InvalidOperationException($"User with ID {user.UserId} not found.");
            }

            users.Remove(existingUser);
            users.Add(user);

            usersAsJson = JsonSerializer.Serialize(users);
            await File.WriteAllTextAsync(filePath, usersAsJson);
        }

        public async Task DeleteAsync(int id)
        {
            string usersAsJson = await File.ReadAllTextAsync(filePath);
            List<User> users = JsonSerializer.Deserialize<List<User>>(usersAsJson) ?? new List<User>();

            var userToRemove = users.SingleOrDefault(u => u.UserId == id);
            if (userToRemove == null)
            {
                throw new InvalidOperationException($"User with ID {id} not found.");
            }

            users.Remove(userToRemove);

            usersAsJson = JsonSerializer.Serialize(users);
            await File.WriteAllTextAsync(filePath, usersAsJson);
        }

        public async Task<User> GetSingleAsync(int id)
        {
            string usersAsJson = await File.ReadAllTextAsync(filePath);
            List<User> users = JsonSerializer.Deserialize<List<User>>(usersAsJson) ?? new List<User>();

            return users.SingleOrDefault(u => u.UserId == id);
        }

        public IQueryable<User> GetMany()
        {
            string usersAsJson = File.ReadAllTextAsync(filePath).Result;
            List<User> users = JsonSerializer.Deserialize<List<User>>(usersAsJson) ?? new List<User>();

            return users.AsQueryable();
        }
        
        public async Task<bool> UserExistsAsync(string username)
        {
            string usersAsJson = await File.ReadAllTextAsync(filePath);
            List<User> users = JsonSerializer.Deserialize<List<User>>(usersAsJson) ?? new List<User>();

            return users.Any(u => u.Username.Equals(username, StringComparison.OrdinalIgnoreCase));
        }
        
        public async Task<User> GetByIdAsync(int id)
        {
            string usersAsJson = await File.ReadAllTextAsync(filePath);
            List<User> users = JsonSerializer.Deserialize<List<User>>(usersAsJson) ?? new List<User>();
    
            var user = users.SingleOrDefault(u => u.UserId == id);
            if (user == null)
            {
                throw new InvalidOperationException($"User with ID {id} not found.");
            }
            return user;
        }


    }
    
    
    
}
