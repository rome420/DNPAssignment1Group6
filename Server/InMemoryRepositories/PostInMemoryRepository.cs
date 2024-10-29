using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Entities;
using RepositoryContracts;

namespace InMemoryRepositories
{
    public class PostInMemoryRepository : IPostRepository
    {
        private readonly List<Post> _posts = new List<Post>();
        private readonly List<User> _users; // Simulating the user repository for Author linking

        // Constructor accepting a list of users, since posts need to have valid authors
        public PostInMemoryRepository(List<User> users)
        {
            _users = users;
        }

        // Adds a new post
        public async Task<Post> AddAsync(Post post)
        {
            if (post == null)
            {
                throw new ArgumentNullException(nameof(post));
            }

            // Ensure that the post has a valid author
            post.Author = _users.SingleOrDefault(u => u.UserId == post.Author.UserId);
            if (post.Author == null)
            {
                throw new InvalidOperationException($"Author with ID '{post.Author.UserId}' not found.");
            }

            // Set PostId
            post.PostId = _posts.Any() ? _posts.Max(p => p.PostId) + 1 : 1;
            _posts.Add(post);

            return await Task.FromResult(post);
        }

        // Updates an existing post
        public async Task UpdateAsync(Post post)
        {
            if (post == null)
            {
                throw new ArgumentNullException(nameof(post));
            }

            var existingPost = _posts.SingleOrDefault(p => p.PostId == post.PostId);
            if (existingPost == null)
            {
                throw new InvalidOperationException($"Post with ID '{post.PostId}' not found.");
            }

            // Ensure that the updated post has a valid author
            existingPost.Author = _users.SingleOrDefault(u => u.UserId == post.Author.UserId);
            if (existingPost.Author == null)
            {
                throw new InvalidOperationException($"Author with ID '{post.Author.UserId}' not found.");
            }

            // Update the post content
            existingPost.Title = post.Title;
            existingPost.Body = post.Body;

            await Task.CompletedTask;
        }

        // Deletes a post by ID
        public async Task DeleteAsync(int id)
        {
            var postToRemove = _posts.SingleOrDefault(p => p.PostId == id);
            if (postToRemove == null)
            {
                throw new InvalidOperationException($"Post with ID '{id}' not found.");
            }

            _posts.Remove(postToRemove);
            await Task.CompletedTask;
        }

        // Gets a single post by ID
        public async Task<Post> GetSingleAsync(int id)
        {
            var post = _posts.SingleOrDefault(p => p.PostId == id);
            if (post == null)
            {
                throw new InvalidOperationException($"Post with ID '{id}' not found.");
            }

            return await Task.FromResult(post);
        }

        // Returns all posts as IQueryable
        public IQueryable<Post> GetMany()
        {
            return _posts.AsQueryable();
        }

        // Retrieves a post by its PostId (alternate name for GetSingleAsync)
        public async Task<Post> GetPostByIdAsync(int postId)
        {
            return await GetSingleAsync(postId);
        }

        // Gets all posts as an IEnumerable
        public Task<IEnumerable<Post>> GetPostsAsync()
        {
            return Task.FromResult(_posts.AsEnumerable());
        }
    }
}
