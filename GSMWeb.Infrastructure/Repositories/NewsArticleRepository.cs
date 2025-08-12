using GSMWeb.Core.Entities;
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

        public async Task<IEnumerable<NewsArticle>> GetAllPublishedAsync()
        {
            return await _context.NewsArticles
                .Where(a => a.IsPublished)
                .OrderByDescending(a => a.PublishedDate)
                .ToListAsync();
        }
    }
}