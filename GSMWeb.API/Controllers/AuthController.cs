using System.Security.Claims;
using GSMWeb.API.DTOs;
using GSMWeb.Core.Entities;
using GSMWeb.Core.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GSMWeb.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] User user, [FromQuery] string password)
        {
            if (user == null || string.IsNullOrEmpty(password))
            {
                return BadRequest("User data and password are required.");
            }

            var createdUser = await _authService.RegisterAsync(user, password);
            return Ok(new { UserId = createdUser.Id, Email = createdUser.Email, Message = "Registration successful." });
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDto loginDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var (isSuccess, message, jwtToken, refreshToken) = await _authService.LoginAsync(loginDto.Email, loginDto.Password);

            if (!isSuccess)
            {
                // Return an unauthorized response with the failure message
                return Unauthorized(new AuthResponseDto { Message = message, IsSuccess = false });
            }

            // Return a 200 OK response with the success message and tokens
            return Ok(new AuthResponseDto
            {
                Message = message,
                Token = jwtToken,
                RefreshToken = refreshToken,
                IsSuccess = true
            });
        }

        [HttpPost("logout")]
        [Authorize] // IMPORTANT: This endpoint requires a valid JWT
        public async Task<IActionResult> Logout()
        {
            // Get the user's ID from the claims in their JWT token
            var userIdString = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (!int.TryParse(userIdString, out var userId))
            {
                return BadRequest("Invalid token.");
            }

            var (isSuccess, message) = await _authService.LogoutAsync(userId);

            if (!isSuccess)
            {
                return BadRequest(new { Message = message });
            }

            return Ok(new { Message = message });
        }
    }
}