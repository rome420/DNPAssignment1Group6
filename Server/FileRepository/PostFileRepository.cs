using System.Text.Json;
using Entities;
using RepositoryContracts;

namespace FileRepository
{
    public class PostFileRepository : IPostRepository
    {
        private readonly string filePath = "posts.json";

        public PostFileRepository()
        {
            if (!File.Exists(filePath))
            {
                File.WriteAllText(filePath, "[]");
            }
        }

        public async Task<Post> AddAsync(Post post)
        {
            string postsAsJson = await File.ReadAllTextAsync(filePath);
            List<Post> posts = JsonSerializer.Deserialize<List<Post>>(postsAsJson) ?? new List<Post>();

            int maxId = posts.Count > 0 ? posts.Max(p => p.PostId) : 0;
            post.PostId = maxId + 1;
            posts.Add(post);

            postsAsJson = JsonSerializer.Serialize(posts);
            await File.WriteAllTextAsync(filePath, postsAsJson);

            return post;
        }

        public async Task<Post> GetPostByIdAsync(int postId)
        {
            string postsAsJson = await File.ReadAllTextAsync(filePath);
            List<Post> posts = JsonSerializer.Deserialize<List<Post>>(postsAsJson) ?? new List<Post>();
            
            var post = posts.SingleOrDefault(p => p.PostId == postId);
            
            return post;
        }


        public async Task<IEnumerable<Post>> GetPostsAsync()
        {
            string postsAsJson = await File.ReadAllTextAsync(filePath);
            List<Post> posts = JsonSerializer.Deserialize<List<Post>>(postsAsJson) ?? new List<Post>();

            return posts;
        }

        public async Task UpdateAsync(Post post)
        {
            string postsAsJson = await File.ReadAllTextAsync(filePath);
            List<Post> posts = JsonSerializer.Deserialize<List<Post>>(postsAsJson) ?? new List<Post>();

            var existingPost = posts.SingleOrDefault(p => p.PostId == post.PostId);
            if (existingPost == null)
            {
                throw new InvalidOperationException($"Post with ID {post.PostId} not found.");
            }

            posts.Remove(existingPost);
            posts.Add(post);

            postsAsJson = JsonSerializer.Serialize(posts);
            await File.WriteAllTextAsync(filePath, postsAsJson);
        }

        public async Task DeleteAsync(int id)
        {
            string postsAsJson = await File.ReadAllTextAsync(filePath);
            List<Post> posts = JsonSerializer.Deserialize<List<Post>>(postsAsJson) ?? new List<Post>();

            var postToRemove = posts.SingleOrDefault(p => p.PostId == id);
            if (postToRemove == null)
            {
                throw new InvalidOperationException($"Post with ID {id} not found.");
            }

            posts.Remove(postToRemove);

            postsAsJson = JsonSerializer.Serialize(posts);
            await File.WriteAllTextAsync(filePath, postsAsJson);
        }

        public async Task<Post> GetSingleAsync(int id)
        {
            string postsAsJson = await File.ReadAllTextAsync(filePath);
            List<Post> posts = JsonSerializer.Deserialize<List<Post>>(postsAsJson) ?? new List<Post>();

            return posts.SingleOrDefault(p => p.PostId == id);
        }

        public IQueryable<Post> GetMany()
        {
            string postsAsJson = File.ReadAllTextAsync(filePath).Result;
            List<Post> posts = JsonSerializer.Deserialize<List<Post>>(postsAsJson) ?? new List<Post>();

            return posts.AsQueryable();
        }
    }
}
