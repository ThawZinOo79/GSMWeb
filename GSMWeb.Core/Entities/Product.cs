using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GSMWeb.Core.Entities
{
    public class Product : BaseEntity
    {
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
        public int? CategoryId { get; set; }
        public ProductCategory? Category { get; set; }
    }
}
