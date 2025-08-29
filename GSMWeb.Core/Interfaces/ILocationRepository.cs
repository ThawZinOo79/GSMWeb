using GSMWeb.Core.Entities;
using GSMWeb.Core.Helpers;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GSMWeb.Core.Interfaces
{
    public interface ILocationRepository : IRepository<Location>
    {
        Task<(IEnumerable<Location> Locations, int TotalCount)> GetPaginatedAndSearchedAsync(
            PagingParameters pagingParams, string? searchTerm);
    }
}