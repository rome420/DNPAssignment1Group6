using DTO;
using Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RepositoryContracts;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        private readonly IPostRepository _postRepository;
        private readonly ILogger<UsersController> _logger;

        public UsersController(IUserRepository userRepository, IPostRepository postRepository, ILogger<UsersController> logger)
        {
            _userRepository = userRepository;
            _postRepository = postRepository;
            _logger = logger;
        }

        // Get all users
        [HttpGet]
        public async Task<ActionResult<List<UserDto>>> GetUsers()
        {
            _logger.LogInformation("Fetching all users.");
            var users = await _userRepository.GetUsersAsync();

            if (users == null || !users.Any())
            {
                _logger.LogWarning("No users found.");
                return NotFound("No users found.");
            }

            var dtos = users.Select(user => new UserDto
            {
                UserId = user.UserId,
                Username = user.Username,
                Posts = user.Posts?.Select(p => new PostDto
                {
                    PostId = p.PostId,
                    Title = p.Title,
                    Body = p.Body
                }).ToList() ?? new List<PostDto>()
            }).ToList();

            return Ok(dtos);
        }

        // Create a new user
        [HttpPost]
        public async Task<ActionResult<UserDto>> CreateUser([FromBody] CreateUserDto request)
        {
            if (request == null)
            {
                _logger.LogError("CreateUser called with null request.");
                return BadRequest("Request cannot be null.");
            }

            _logger.LogInformation($"Creating user with username: {request.Username}");

            var existingUser = (await _userRepository.GetUsersAsync())
                                .FirstOrDefault(u => u.Username.Equals(request.Username, StringComparison.OrdinalIgnoreCase));
            if (existingUser != null)
            {
                _logger.LogWarning($"Username '{request.Username}' is already taken.");
                return Conflict($"Username '{request.Username}' is already taken.");
            }

            var user = new User
            {
                Username = request.Username,
                Password = request.Password
            };

            var addedUser = await _userRepository.AddAsync(user);
            return Created($"/Users/{addedUser.UserId}", new UserDto
            {
                UserId = addedUser.UserId,
                Username = addedUser.Username,
                Posts = addedUser.Posts?.Select(p => new PostDto
                {
                    PostId = p.PostId,
                    Title = p.Title,
                    Body = p.Body
                }).ToList() ?? new List<PostDto>()
            });
        }

        // Get a single user by ID
        [HttpGet("{id:int}")]
        public async Task<ActionResult<UserDto>> GetSingleUser([FromRoute] int id)
        {
            _logger.LogInformation($"Fetching user with ID: {id}");
            var user = await _userRepository.GetByIdAsync(id);
            if (user == null)
            {
                _logger.LogWarning($"User with ID {id} was not found.");
                return NotFound($"User with ID {id} was not found.");
            }

            return Ok(new UserDto
            {
                UserId = user.UserId,
                Username = user.Username,
                Posts = user.Posts?.Select(p => new PostDto
                {
                    PostId = p.PostId,
                    Title = p.Title,
                    Body = p.Body
                }).ToList() ?? new List<PostDto>()
            });
        }

        // Update an existing user
        [HttpPatch("{id:int}")]
        public async Task<ActionResult<UserDto>> UpdateUser([FromRoute] int id, [FromBody] UpdateUserDto request)
        {
            if (request == null)
            {
                _logger.LogError("UpdateUser called with null request.");
                return BadRequest("Request cannot be null.");
            }

            _logger.LogInformation($"Updating user with ID: {id}");

            var existingUser = await _userRepository.GetByIdAsync(id);
            if (existingUser == null)
            {
                _logger.LogWarning($"User with ID {id} not found for update.");
                return NotFound($"User with ID {id} was not found.");
            }

            existingUser.Username = request.Username;
            existingUser.Password = request.Password;

            await _userRepository.UpdateAsync(existingUser);

            return Ok(new UserDto
            {
                UserId = existingUser.UserId,
                Username = existingUser.Username,
                Posts = existingUser.Posts?.Select(p => new PostDto
                {
                    PostId = p.PostId,
                    Title = p.Title,
                    Body = p.Body
                }).ToList() ?? new List<PostDto>()
            });
        }

        // Delete a user by ID
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteUser([FromRoute] int id)
        {
            _logger.LogInformation($"Deleting user with ID: {id}");

            var existingUser = await _userRepository.GetByIdAsync(id);
            if (existingUser == null)
            {
                _logger.LogWarning($"User with ID {id} not found for deletion.");
                return NotFound($"User with ID {id} was not found.");
            }

            await _userRepository.DeleteAsync(id);
            return NoContent();
        }
    }
}