using GSMWeb.Core.Entities;
using GSMWeb.Core.Helpers;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GSMWeb.Core.Interfaces
{
    public interface INewsArticleRepository : IRepository<NewsArticle>
    {
        Task<(IEnumerable<NewsArticle> Articles, int TotalCount)> GetPaginatedAndSearchedPublishedAsync(
            PagingParameters pagingParams, string? searchTerm);
    }
}