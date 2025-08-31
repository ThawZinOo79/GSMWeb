using GSMWeb.Core.Entities;
using GSMWeb.Core.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GSMWeb.Core.Interfaces
{
    public interface IProductService
    {
        Task<(IEnumerable<Product> Products, int TotalCount)> GetPaginatedProductsAsync(PagingParameters pagingParams, string? searchTerm);
        Task<(IEnumerable<Product> Products, int TotalCount)> GetPaginatedProductsByCategoryAsync(int categoryId, PagingParameters pagingParams, string? searchTerm);
        Task<Product?> GetProductByIdAsync(int id);
        Task<Product> CreateProductAsync(Product product);
        Task<bool> UpdateProductAsync(Product product);
        Task<bool> DeleteProductAsync(int id);
    }
}
