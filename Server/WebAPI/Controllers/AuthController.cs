using Microsoft.AspNetCore.Mvc;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;
using DTO;
using System.Linq;
using System.Collections.Generic;
using Microsoft.Extensions.Logging;

namespace YourNamespace.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly ILogger<AuthController> _logger;

        public AuthController(ILogger<AuthController> logger)
        {
            _logger = logger;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest loginRequest)
        {
            try
            {
                _logger.LogInformation("Login attempt for user: {Username}", loginRequest.Username);

                var users = await System.IO.File.ReadAllTextAsync("C:/Users/peeta/Desktop/GIT REPOSITORY/DNPAssignment1Group6/Server/WebAPI/users.json");
                var userList = JsonSerializer.Deserialize<List<UserDto>>(users);

                var user = userList.FirstOrDefault(u => u.Username == loginRequest.Username && u.Password == loginRequest.Password);
                if (user != null)
                {
                    _logger.LogInformation("User {Username} found", user.Username);
                    return Ok(new UserDto { Username = user.Username, UserId = user.UserId });
                }

                _logger.LogWarning("User {Username} not found", loginRequest.Username);
                return NotFound("User not found");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred during login for user: {Username}", loginRequest.Username);
                return StatusCode(500, "Internal server error");
            }
        }
    }
}