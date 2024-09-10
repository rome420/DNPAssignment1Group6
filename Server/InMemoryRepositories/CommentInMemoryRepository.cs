using Entities;
using RepositoryContracts;
namespace InMemoryRepositories;

public class CommentInMemoryRepository : ICommentRepository
    {
        private readonly List<Comment> _comments = new List<Comment>();
        private readonly List<Post> _posts = new List<Post>();  
        public async Task<Comment> AddAsync(Comment comment)
        {
            if (comment == null)
            {
                throw new ArgumentNullException(nameof(comment));
            }

            
            if (comment.Post == null || !_posts.Any(p => p.PostId == comment.Post.PostId))
            {
                throw new ArgumentException($"Post with ID {comment.Post?.PostId} does not exist.");
            }

            comment.CommentId = _comments.Any() 
                ? _comments.Max(c => c.CommentId) + 1
                : 1;
            _comments.Add(comment);
            return await Task.FromResult(comment);
        }

        public async Task UpdateAsync(Comment comment)
        {
            if (comment == null)
            {
                throw new ArgumentNullException(nameof(comment));
            }

            var existingComment = _comments.SingleOrDefault(c => c.CommentId == comment.CommentId);
            if (existingComment == null)
            {
                throw new InvalidOperationException($"Comment with ID '{comment.CommentId}' not found.");
            }

           
            if (comment.Post == null || !_posts.Any(p => p.PostId == comment.Post.PostId))
            {
                throw new ArgumentException($"Post with ID {comment.Post?.PostId} does not exist.");
            }

            _comments.Remove(existingComment);
            _comments.Add(comment);

            await Task.CompletedTask;
        }

        public async Task DeleteAsync(int id)
        {
            var commentToRemove = _comments.SingleOrDefault(c => c.CommentId == id);
            if (commentToRemove == null)
            {
                throw new InvalidOperationException($"Comment with ID '{id}' not found.");
            }

            _comments.Remove(commentToRemove);
            await Task.CompletedTask;
        }

        public async Task<Comment> GetSingleAsync(int id)
        {
            var comment = _comments.SingleOrDefault(c => c.CommentId == id);
            if (comment == null)
            {
                throw new InvalidOperationException($"Comment with ID '{id}' not found.");
            }

            return await Task.FromResult(comment);
        }

        public IQueryable<Comment> GetMany()
        {
            return _comments.AsQueryable();
        }

        
    }
