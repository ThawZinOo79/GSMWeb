using GSMWeb.Core.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GSMWeb.Core.Interfaces
{
    public interface INewsArticleRepository : IRepository<NewsArticle>
    {
        // Get all published articles, newest first
        Task<IEnumerable<NewsArticle>> GetAllPublishedAsync();
    }
}