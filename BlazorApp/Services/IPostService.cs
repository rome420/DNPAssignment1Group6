using DTO;

namespace BlazorApp.Services;

public interface IPostService
{
    Task<IEnumerable<PostDto>> GetAllPostsAsync();
    Task<PostDto> GetPostByIdAsync(int id);
    Task AddPostAsync(CreatePostDto post);
    Task UpdatePostAsync(int id, UpdatePostDto post);
    Task DeletePostAsync(int id);
    Task AddCommentAsync(int postId, CreateCommentDto createCommentDto);
}
