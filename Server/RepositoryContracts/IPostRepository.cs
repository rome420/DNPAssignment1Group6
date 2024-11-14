using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Entities;

namespace RepositoryContracts
{
    public interface IPostRepository
    {
        Task<Post> AddAsync(Post post);
        Task UpdateAsync(Post post);
        Task DeleteAsync(int id);
        IQueryable<Post> GetMany();
        Task<Post?> GetPostByIdAsync(int postId);  // Ensure it returns a nullable Post
        Task<IEnumerable<Post>> GetPostsAsync();
    }
}