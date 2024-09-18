using RepositoryContracts;

namespace CLI.ManageUsers;

public class ListUsersView
{
    private readonly IUserRepository _userRepository;

    public ListUsersView(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task Show()
    {
        var users = await _userRepository.GetUsersAsync();
        foreach (var user in users)
        {
            Console.WriteLine($"{user.UserId}: {user.Username}");
        }
    }

}
