using Entities;
using RepositoryContracts;

namespace CLI.ManagePosts;

public class CreatePostView
{
    private readonly IPostRepository _postRepository;

    public CreatePostView(IPostRepository postRepository)
    {
        _postRepository = postRepository;
    }

    public async Task Show()
    {
        Console.WriteLine("Enter post title:");
        var title = Console.ReadLine();
        
        Console.WriteLine("Enter post body:");
        var body = Console.ReadLine();

        var post = new Post { Title = title, Body = body };
        await _postRepository.AddAsync(post);


        Console.WriteLine("Post created!");
    }
}
