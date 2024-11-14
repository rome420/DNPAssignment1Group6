using DTO;
using Entities;
using Microsoft.AspNetCore.Mvc;
using RepositoryContracts;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PostsController : ControllerBase
    {
        private readonly IPostRepository _postRepository;
        private readonly IUserRepository _userRepository;

        public PostsController(IPostRepository postRepository, IUserRepository userRepository)
        {
            _postRepository = postRepository;
            _userRepository = userRepository;
        }

        // Create a new post
        [HttpPost]
        public async Task<ActionResult<PostDto>> CreatePost([FromBody] CreatePostDto request)
        {
            var user = await _userRepository.GetByIdAsync(request.UserId);
            if (user == null)
            {
                return NotFound($"User with ID {request.UserId} not found.");
            }

            var post = new Post
            {
                Title = request.Title,
                Body = request.Body,
                Author = user
            };

            var addedPost = await _postRepository.AddAsync(post);

            user.Posts.Add(addedPost);
            await _userRepository.UpdateAsync(user);

            return Created($"/Posts/{addedPost.PostId}", new PostDto
            {
                PostId = addedPost.PostId,
                Title = addedPost.Title,
                Body = addedPost.Body,
                Author = user.Username
            });
        }

        // Get all posts
        [HttpGet]
        public async Task<ActionResult<List<PostDto>>> GetPosts()
        {
            var posts = await _postRepository.GetPostsAsync();

            var dtos = posts.Select(post => new PostDto
            {
                PostId = post.PostId,
                Title = post.Title,
                Body = post.Body,
                Author = post.Author?.Username ?? "Unknown"
            }).ToList();

            return Ok(dtos);
        }

        // Get a single post by ID
        [HttpGet("{id:int}")]
        public async Task<ActionResult<PostDto>> GetSinglePost([FromRoute] int id)
        {
            var post = await _postRepository.GetPostByIdAsync(id);
            if (post == null)
            {
                return NotFound($"Post with ID {id} not found.");
            }

            var postDto = new PostDto
            {
                PostId = post.PostId,
                Title = post.Title,
                Body = post.Body,
                Author = post.Author?.Username ?? "Unknown",
                Comments = post.Comments.Select(c => new CommentDto
                {
                    CommentId = c.CommentId,
                    Text = c.Text,
                    Author = c.Author?.Username ?? "Unknown"
                }).ToList()
            };

            return Ok(postDto);
        }

        // Update an existing post
        [HttpPatch("{id:int}")]
        public async Task<ActionResult<PostDto>> UpdatePost([FromRoute] int id, [FromBody] UpdatePostDto request)
        {
            var existingPost = await _postRepository.GetPostByIdAsync(id);
            if (existingPost == null)
            {
                return NotFound($"Post with ID {id} not found.");
            }

            existingPost.Title = request.Title;
            existingPost.Body = request.Body;

            await _postRepository.UpdateAsync(existingPost);

            return Ok(new PostDto
            {
                PostId = existingPost.PostId,
                Title = existingPost.Title,
                Body = existingPost.Body,
                Author = existingPost.Author?.Username ?? "Unknown"
            });
        }

        // Delete a post by ID
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeletePost([FromRoute] int id)
        {
            var existingPost = await _postRepository.GetPostByIdAsync(id);
            if (existingPost == null)
            {
                return NotFound($"Post with ID {id} not found.");
            }

            await _postRepository.DeleteAsync(id);
            return NoContent();
        }
    }
}