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
        public async Task<IActionResult> Register([FromBody] RegisterDto registerDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var user = new User
            {
                Name = registerDto.Name,
                Email = registerDto.Email,
                PhoneNumber = registerDto.PhoneNumber,
                Role = registerDto.Role
            };

            var createdUser = await _authService.RegisterAsync(user, registerDto.Password);
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
                return Unauthorized(new AuthResponseDto { Message = message, IsSuccess = false });
            }

            return Ok(new AuthResponseDto
            {
                Message = message,
                Token = jwtToken,
                RefreshToken = refreshToken,
                IsSuccess = true
            });
        }

        [HttpPost("logout")]
        [Authorize]
        public async Task<IActionResult> Logout()
        {
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