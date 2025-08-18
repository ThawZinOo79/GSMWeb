using System.ComponentModel.DataAnnotations.Schema;

namespace GSMWeb.Core.Entities
{
    public class CompanyProfile : BaseEntity
    {
        public required string CompanyName { get; set; }
        public required string Description { get; set; }
        public required string HoAddress { get; set; }
        public string? Address1 { get; set; }
        public string? Address2 { get; set; }
        public string? FacebookLink { get; set; }
        public string? YoutubeLink { get; set; }
        public string? InstagramLink { get; set; }
        public string? TiktokLink { get; set; }
        public string? InfoMail { get; set; }
        public string? HrMail { get; set; }
        public required string HotlinePh { get; set; }
        public string? Phone1 { get; set; }
        public string? Phone2 { get; set; }
        public required string Email { get; set; }
        public string? PhotoUrl { get; set; }
        public required string AboutUs { get; set; }
        public DateTime PublishDate { get; set; }

        [ForeignKey("User")]
        public int UserId { get; set; }
        public virtual User? User { get; set; }
    }
}