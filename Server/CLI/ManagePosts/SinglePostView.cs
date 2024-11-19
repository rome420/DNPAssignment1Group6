using System;
using System.Threading.Tasks;
using RepositoryContracts;
using Entities;

namespace CLI.ManagePosts
{
    public class SinglePostView
    {
        private readonly IPostRepository _postRepository;
        private readonly ICommentRepository _commentRepository;

        public SinglePostView(IPostRepository postRepository, ICommentRepository commentRepository)
        {
            _postRepository = postRepository;
            _commentRepository = commentRepository;
        }

        public async Task Show()
        {
            Console.WriteLine("Enter post ID:");
            var postId = int.Parse(Console.ReadLine());

            var post = await _postRepository.GetPostByIdAsync(postId);
            if (post != null)
            {
                Console.WriteLine($"Title: {post.Title}");
                Console.WriteLine($"Body: {post.Body}");

                var comments = await _commentRepository.GetCommentsByPostIdAsync(postId);
                Console.WriteLine("Comments:");
                foreach (var comment in comments)
                {
                    Console.WriteLine($"{comment.PostId}: {comment.Body}");
                }
            }
            else
            {
                Console.WriteLine("Post not found.");
            }
        }
    }
}