namespace GSMWeb.Core.Entities
{
    public class NewsArticle : BaseEntity
    {
        public required string Title { get; set; }
        public required string Summary { get; set; } // The short text on the list page
        public required string ContentBody { get; set; } // The full content of the news article
        public required string ImageUrl { get; set; }
        public DateTime PublishedDate { get; set; }
        public bool IsPublished { get; set; } = false; // To control visibility
    }
}