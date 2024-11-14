using DTO;
using Entities;
using Microsoft.AspNetCore.Mvc;
using RepositoryContracts;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("api/posts/{postId}/comments")]
    public class CommentsController : ControllerBase
    {
        private readonly ICommentRepository _commentRepository;

        public CommentsController(ICommentRepository commentRepository)
        {
            _commentRepository = commentRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CommentDto>>> GetComments(int postId)
        {
            var comments = await _commentRepository.GetCommentsByPostIdAsync(postId);

            if (comments == null || !comments.Any())
            {
                return NotFound($"No comments found for post with ID {postId}.");
            }

            var commentDtos = comments.Select(c => new CommentDto
            {
                CommentId = c.CommentId,
                Text = c.Text,
                Author = c.Author.Username
            }).ToList();

            return Ok(commentDtos);
        }

        [HttpPost]
        public async Task<ActionResult<CommentDto>> AddComment([FromBody] CreateCommentDto request)
        {
            var comment = new Comment
            {
                Text = request.Text,
                Author = new User { Username = request.Author }, // Assuming Author is a string in CreateCommentDto
                PostId = request.PostId
            };

            await _commentRepository.AddAsync(comment);

            return CreatedAtAction(nameof(GetComments), new { postId = request.PostId }, new CommentDto
            {
                CommentId = comment.CommentId,
                Text = comment.Text,
                Author = comment.Author.Username
            });
        }
    }
}