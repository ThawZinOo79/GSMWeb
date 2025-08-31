using GSMWeb.Core.Entities;
using GSMWeb.Core.Helpers;
using GSMWeb.Core.Interfaces;
using GSMWeb.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GSMWeb.Infrastructure.Repositories
{
    public class ProductRepository : Repository<Product>, IProductRepository
    {
        public ProductRepository(ApplicationDbContext context) : base(context) { }

        public async Task<(IEnumerable<Product> Products, int TotalCount)> GetPaginatedProductsAsync(PagingParameters pagingParams, string? searchTerm)
        {
            var query = _context.Products.Include(p => p.Category).AsQueryable();

            if (!string.IsNullOrEmpty(searchTerm))
            {
                var term = searchTerm.ToLower();
                query = query.Where(p => p.ProductName.ToLower().Contains(term));
            }

            var totalCount = await query.CountAsync();
            var products = await query.Skip((pagingParams.PageNumber - 1) * pagingParams.PageSize)
                                      .Take(pagingParams.PageSize)
                                      .ToListAsync();
            return (products, totalCount);
        }

        public async Task<(IEnumerable<Product> Products, int TotalCount)> GetPaginatedProductsByCategoryAsync(int categoryId, PagingParameters pagingParams, string? searchTerm)
        {
            var query = _context.Products.Include(p => p.Category)
                        .Where(p => p.CategoryId == categoryId);

            if (!string.IsNullOrEmpty(searchTerm))
            {
                var term = searchTerm.ToLower();
                query = query.Where(p => p.ProductName.ToLower().Contains(term));
            }

            var totalCount = await query.CountAsync();
            var products = await query.Skip((pagingParams.PageNumber - 1) * pagingParams.PageSize)
                                      .Take(pagingParams.PageSize)
                                      .ToListAsync();
            return (products, totalCount);
        }
    }



}
