using RepositoryContracts;

namespace CLI.ManageUsers;

public class ManageUsersView
{
    private readonly IUserRepository _userRepository;

    public ManageUsersView(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task ShowMenu()
    {
        Console.WriteLine("User Management");
        Console.WriteLine("1. Create User");
        Console.WriteLine("2. List Users");
        Console.WriteLine("3. Back");
        var choice = Console.ReadLine();

        switch (choice)
        {
            case "1":
                var createUserView = new CreateUserView(_userRepository);
                await createUserView.Show();
                break;
            case "2":
                var listUsersView = new ListUsersView(_userRepository);
                await listUsersView.Show();
                break;
            default:
                break;
        }
    }
}
