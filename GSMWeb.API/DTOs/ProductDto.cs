namespace GSMWeb.API.DTOs
{
    public class ProductDto
    {
        public int Id { get; set; }
        public string ProductName { get; set; } = string.Empty;
        public string? ProductDescription { get; set; }
        public string? GoldQuality { get; set; }
        public string? ProductType { get; set; }
        public int? EstimatePrice { get; set; }
        public string? Specification { get; set; }
        public string? Photo { get; set; }
        public string? User { get; set; }
        public string? CreatedBy { get; set; }
        public string? UpdatedBy { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public int? CategoryId { get; set; }
        public string? CategoryName { get; set; }
    }
}
