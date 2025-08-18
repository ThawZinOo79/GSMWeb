using System.Collections.Generic;

namespace GSMWeb.API.DTOs
{
    public class ContactUsDto
    {
        public string MainAddress { get; set; } = string.Empty;
        public List<string> PhoneNumbers { get; set; } = new();
        public List<string> EmailAddresses { get; set; } = new();
        public Dictionary<string, string?> SocialMediaLinks { get; set; } = new();
    }
}