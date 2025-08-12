using GSMWeb.Core.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GSMWeb.Core.Interfaces
{
    public interface INewsArticleService
    {
        Task<IEnumerable<NewsArticle>> GetAllPublishedArticlesAsync();
        Task<NewsArticle?> GetArticleByIdAsync(int id);
        Task<NewsArticle> CreateArticleAsync(NewsArticle article);
        Task<bool> UpdateArticleAsync(NewsArticle article);
        Task<bool> DeleteArticleAsync(int id);
    }
}