using RepositoryContracts;

namespace CLI.ManagePosts;

public class ManagePostsView
{
    private readonly IPostRepository _postRepository;
    private readonly ICommentRepository _commentRepository;

    public ManagePostsView(IPostRepository postRepository, ICommentRepository commentRepository)
    {
        _postRepository = postRepository;
        _commentRepository = commentRepository;
    }

    public async Task ShowMenu()
    {
        Console.WriteLine("Post Management");
        Console.WriteLine("1. Create Post");
        Console.WriteLine("2. List Posts");
        Console.WriteLine("3. View Post");
        Console.WriteLine("4. Back");
        var choice = Console.ReadLine();

        switch (choice)
        {
            case "1":
                var createPostView = new CreatePostView(_postRepository);
                await createPostView.Show();
                break;
            case "2":
                var listPostsView = new ListPostsView(_postRepository);
                await listPostsView.Show();
                break;
            case "3":
                var singlePostView = new SinglePostView(_postRepository, _commentRepository);
                await singlePostView.Show();
                break;
            default:
                break;
        }
    }
}
