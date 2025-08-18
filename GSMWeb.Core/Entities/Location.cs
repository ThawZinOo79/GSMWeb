using System.ComponentModel.DataAnnotations.Schema;

namespace GSMWeb.Core.Entities
{
    public class Location : BaseEntity
    {
        public required string LocationName { get; set; }

        public required string Address { get; set; }

        public string? MapLink { get; set; }
        public string? Phone { get; set; }
        public string? PhotoUrl { get; set; }

        public string? Remark { get; set; }

        [ForeignKey("User")]
        public int UserId { get; set; }
        public virtual User? User { get; set; }
    }
}