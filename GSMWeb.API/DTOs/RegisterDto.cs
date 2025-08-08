using System.ComponentModel.DataAnnotations;

namespace GSMWeb.API.DTOs
{
    public class RegisterDto
    {
        [Required]
        [StringLength(100)]
        public required string Name { get; set; }

        [Required]
        [EmailAddress]
        public required string Email { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 6)]
        public required string Password { get; set; }

        public string? PhoneNumber { get; set; }

        [Required]
        public required string Role { get; set; } // e.g., "Admin", "User"
    }
}