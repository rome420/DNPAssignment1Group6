using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Entities;
using RepositoryContracts;

namespace FileRepositories
{
    public class CommentFileRepository : ICommentRepository
    {
        private readonly List<Comment> _comments = new List<Comment>();

        public async Task AddAsync(Comment comment)
        {
            comment.CommentId = _comments.Any() ? _comments.Max(c => c.CommentId) + 1 : 1;
            _comments.Add(comment);
            await Task.CompletedTask;
        }

        public Task UpdateAsync(Comment comment)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Comment>> GetCommentsAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Comment?> GetCommentByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Comment?> GetSingleAsync(int id)
        {
            throw new NotImplementedException();
        }

        public IQueryable<Comment> GetMany()
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Comment>> GetCommentsByPostIdAsync(int postId)
        {
            var comments = _comments.Where(c => c.PostId == postId).ToList();
            return await Task.FromResult(comments);
        }

        Task<Comment> ICommentRepository.AddAsync(Comment comment)
        {
            throw new NotImplementedException();
        }
    }
}