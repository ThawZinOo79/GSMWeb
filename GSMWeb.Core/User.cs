namespace GSMWeb.Core.Entities
{
    public class User : BaseEntity
    {
        public required string Name { get; set; }
        public required string Email { get; set; }
        public string Password { get; set; } = string.Empty;
        public string? PhoneNumber { get; set; }
        public required string Role { get; set; }
        public bool IsActive { get; set; } = false;

        public string? RefreshToken { get; set; }
        public DateTime? RefreshTokenExpiryTime { get; set; }
    }
}