using Microsoft.AspNetCore.Mvc;
using RepositoryContracts;
using DTO;
using Entities;

namespace WebAPI.Controllers;

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
            Username = user.Username
        };

        return Ok(userDto);
    }
}