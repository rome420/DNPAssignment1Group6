using Entities;
using RepositoryContracts;

namespace CLI.ManageUsers;

public class CreateUserView
{
    private readonly IUserRepository _userRepository;

    public CreateUserView(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task Show()
    {
        Console.WriteLine("Enter username:");
        var username = Console.ReadLine();
        
        Console.WriteLine("Enter password:");
        var password = Console.ReadLine();
        
        var user = new User { Username = username, Password = password };
        await _userRepository.AddUserAsync(user);
        
        Console.WriteLine($"User {username} created!");
    }
}
