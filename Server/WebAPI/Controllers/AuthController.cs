using DTO;
using Entities;
using Microsoft.AspNetCore.Mvc;
using RepositoryContracts;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IUserRepository _userRepository;

        public AuthController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
        {
            var user = await _userRepository.GetUserByUsernameAsync(loginDto.Username);
            if (user == null)
            {
                return Unauthorized("User does not exist.");
            }

            if (user.Password != loginDto.Password)
            {
                return Unauthorized("Incorrect password.");
            }

            var userDto = new UserDto
            {
                UserId = user.UserId,
                Username = user.Username,
                Posts = (user.Posts ?? new List<Post>()).Select(p => new PostDto
                {
                    PostId = p.PostId,
                    Title = p.Title,
                    Body = p.Body,
                    Author = p.Author.Username,
                    Comments = p.Comments.Select(c => new CommentDto
                    {
                        CommentId = c.CommentId,
                        Text = c.Text,  // Use Text property instead of Content
                        Body = c.Body,
                        PostId = c.PostId,
                        Author = c.Author.Username
                    }).ToList()
                }).ToList()
            };

            return Ok(userDto);
        }
    }
}