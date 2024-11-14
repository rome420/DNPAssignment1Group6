using DTO;
using Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly string userFilePath = "C:/Users/peeta/Desktop/GIT REPOSITORY/DNPAssignment1Group6/users.json";
        private readonly ILogger<AuthController> _logger;

        public AuthController(ILogger<AuthController> logger)
        {
            _logger = logger;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
        {
            try
            {
                var userJson = await System.IO.File.ReadAllTextAsync(userFilePath);
                var users = JsonSerializer.Deserialize<List<User>>(userJson);

                var user = users?.FirstOrDefault(u => u.Username == loginDto.Username && u.Password == loginDto.Password);

                if (user == null)
                {
                    _logger.LogWarning("Invalid username or password.");
                    return Unauthorized("Invalid username or password.");
                }

                var userDto = new UserDto
                {
                    UserId = user.UserId,
                    Username = user.Username,
                    Posts = user.Posts?.Select(p => new PostDto
                    {
                        PostId = p.PostId,
                        Title = p.Title,
                        Body = p.Body
                    }).ToList() ?? new List<PostDto>()
                };

                return Ok(userDto);
            }
            catch (FileNotFoundException)
            {
                _logger.LogError("User file not found.");
                return NotFound("User file not found.");
            }
            catch (JsonException)
            {
                _logger.LogError("Error parsing user file.");
                return BadRequest("Error parsing user file.");
            }
            catch (Exception ex)
            {
                _logger.LogError($"Internal server error: {ex.Message}");
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}