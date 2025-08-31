using GSMWeb.Core.Entities;
using GSMWeb.Core.Helpers;
using GSMWeb.Core.Interfaces;
using GSMWeb.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GSMWeb.Infrastructure.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _repo;
        private readonly ICategoryRepository _categoryRepo;

        public ProductService(IProductRepository repo, ICategoryRepository categoryRepo)
        {
            _repo = repo;
            _categoryRepo = categoryRepo;
        }
        
        public async Task<Product> CreateProductAsync(Product product)
        {
            var category = await _categoryRepo.GetByIdAsync(product.CategoryId ?? 0);
            if (category == null)
            {
                return null; // return null instead of throwing exception
            }

            await _repo.AddAsync(product);
            await _repo.SaveChangesAsync();
            return product;
        }

        public async Task<bool> DeleteProductAsync(int id)
        {
            var product = await _repo.GetByIdAsync(id);
            if (product == null) return false;
            _repo.Delete(product);
            await _repo.SaveChangesAsync();
            return true;
        }

        public async Task<(IEnumerable<Product> Products, int TotalCount)> GetPaginatedProductsAsync(PagingParameters pagingParams, string? searchTerm)
        {
            return await _repo.GetPaginatedProductsAsync(pagingParams, searchTerm);
        }

        public async Task<(IEnumerable<Product> Products, int TotalCount)> GetPaginatedProductsByCategoryAsync(int categoryId, PagingParameters pagingParams, string? searchTerm)
        {
            return await _repo.GetPaginatedProductsByCategoryAsync(categoryId, pagingParams, searchTerm);
        }

        public async Task<Product?> GetProductByIdAsync(int id) => await _repo.GetByIdAsync(id);

        public async Task<bool> UpdateProductAsync(Product product)
        {
            var existing = await _repo.GetByIdAsync(product.Id);
            if (existing == null) return false;

            existing.ProductName = product.ProductName;
            existing.ProductDescription = product.ProductDescription;
            existing.GoldQuality = product.GoldQuality;
            existing.ProductType = product.ProductType;
            existing.EstimatePrice = product.EstimatePrice;
            existing.Specification = product.Specification;
            existing.Photo = product.Photo;
            existing.User = product.User;
            existing.UpdatedAt = DateTime.UtcNow;
            existing.CategoryId = product.CategoryId;

            _repo.Update(existing);
            await _repo.SaveChangesAsync();
            return true;
        }
    }

}
