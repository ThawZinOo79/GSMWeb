namespace GSMWeb.Core.Entities
{
    public class NewsArticle : BaseEntity
    {
        public required string Title { get; set; }
        public required string Summary { get; set; }
        public required string ContentBody { get; set; }
        public required string ImageUrl { get; set; }
        public DateTime PublishedDate { get; set; }
        public bool IsPublished { get; set; } = false;
    }
}