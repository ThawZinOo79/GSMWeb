using GSMWeb.Core.Entities;
using GSMWeb.Core.Helpers;
using GSMWeb.Core.Interfaces;
using GSMWeb.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GSMWeb.Infrastructure.Repositories
{
    public class NewsArticleRepository : Repository<NewsArticle>, INewsArticleRepository
    {
        public NewsArticleRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<(IEnumerable<NewsArticle> Articles, int TotalCount)> GetPaginatedAndSearchedPublishedAsync(
            PagingParameters pagingParams, string? searchTerm)
        {
            var query = _context.NewsArticles
                .Where(a => a.IsPublished)
                .OrderByDescending(a => a.PublishedDate)
                .AsQueryable();

            if (!string.IsNullOrEmpty(searchTerm))
            {
                var lowerCaseSearchTerm = searchTerm.Trim().ToLower();
                query = query.Where(a =>
                    a.Title.ToLower().Contains(lowerCaseSearchTerm) ||
                    a.Summary.ToLower().Contains(lowerCaseSearchTerm) ||
                    a.ContentBody.ToLower().Contains(lowerCaseSearchTerm));
            }

            var totalCount = await query.CountAsync();

            var pagedArticles = await query
                .Skip((pagingParams.PageNumber - 1) * pagingParams.PageSize)
                .Take(pagingParams.PageSize)
                .ToListAsync();

            return (pagedArticles, totalCount);
        }
    }
}