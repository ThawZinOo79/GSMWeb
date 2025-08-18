using System.ComponentModel.DataAnnotations;

namespace GSMWeb.API.DTOs
{
    public class RegisterDto
    {
        [Required]
        public required string Name { get; set; }

        [Required]
        [EmailAddress]
        public required string Email { get; set; }

        [Required]
        [MinLength(6)]
        public required string Password { get; set; }

        public string? PhoneNumber { get; set; }

        [Required]
        public required string Role { get; set; }
    }
}