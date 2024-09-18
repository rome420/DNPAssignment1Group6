using RepositoryContracts;

namespace CLI.ManagePosts;

public class ListPostsView
{
    private readonly IPostRepository _postRepository;

    public ListPostsView(IPostRepository postRepository)
    {
        _postRepository = postRepository;
    }

    public async Task Show()
    {
        var posts = await _postRepository.GetPostsAsync();
        foreach (var post in posts)
        {
            Console.WriteLine($"{post.PostId}: {post.Title}");

        }
    }
}
