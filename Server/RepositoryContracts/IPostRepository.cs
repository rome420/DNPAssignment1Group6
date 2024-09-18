using System.Collections;
using Entities;

namespace RepositoryContracts
{
    public interface IPostRepository
    {
        Task<Post> AddAsync(Post post);
        Task UpdateAsync(Post post);
        Task DeleteAsync(int id);
        Task<Post> GetSingleAsync(int id);  // If you still need this method.
        IQueryable<Post> GetMany();
        Task<Post> GetPostByIdAsync(int postId);  // This needs to return a Post
        Task<IEnumerable<Post>> GetPostsAsync();

    }
}