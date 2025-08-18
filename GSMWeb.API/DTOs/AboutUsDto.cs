namespace GSMWeb.API.DTOs
{
    public class AboutUsDto
    {
        public string CompanyName { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string AboutUsText { get; set; } = string.Empty;
        public string? PhotoUrl { get; set; }
    }
}