using CLI.ManagePosts;
using CLI.ManageUsers;
using RepositoryContracts;

namespace CLI.UI;

public class CliApp
{
    private readonly IUserRepository _userRepository;
    private readonly IPostRepository _postRepository;
    private readonly ICommentRepository _commentRepository;

    public CliApp(IUserRepository userRepository, IPostRepository postRepository, ICommentRepository commentRepository)
    {
        _userRepository = userRepository;
        _postRepository = postRepository;
        _commentRepository = commentRepository;
    }

    public CliApp(IUserRepository userRepository, ICommentRepository commentRepository, IPostRepository postRepository)
    {
        throw new NotImplementedException();
    }

    public async Task StartAsync()
    {
        Console.WriteLine("Welcome to the CLI app!");

        while (true)
        {
            Console.WriteLine("1. Manage Users");
            Console.WriteLine("2. Manage Posts");
            Console.WriteLine("3. Exit");
            var choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    var manageUsersView = new ManageUsersView(_userRepository);
                    await manageUsersView.ShowMenu();
                    break;
                case "2":
                    var managePostsView = new ManagePostsView(_postRepository, _commentRepository);
                    await managePostsView.ShowMenu();
                    break;
                case "3":
                    return;
                default:
                    Console.WriteLine("Invalid option.");
                    break;
            }
        }
    }
}
