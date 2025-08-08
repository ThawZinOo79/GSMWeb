namespace GSMWeb.Core.Entities
{
    public class User : BaseEntity
    {
        public required string Name { get; set; }
        public required string Email { get; set; }
        public required string Password { get; set; } // IMPORTANT: This will be a hashed password, not plain text.
        public string? PhoneNumber { get; set; }
        public required string Role { get; set; }
        public bool IsActive { get; set; } = false;
    }
}