using System.Collections.Generic;
using System.Threading.Tasks;
using Entities;

namespace RepositoryContracts
{
    public interface ICommentRepository
    {
        Task<Comment?> GetSingleAsync(int id);
        IQueryable<Comment> GetMany();
        Task<IEnumerable<Comment>> GetCommentsByPostIdAsync(int postId);
        Task<Comment> AddAsync(Comment comment);
        Task UpdateAsync(Comment comment);
        Task DeleteAsync(int id);
        Task<IEnumerable<Comment>> GetCommentsAsync(); // Add this method
        Task<Comment?> GetCommentByIdAsync(int id); // Add this method
    }
}