using GSMWeb.Core.Entities;
using GSMWeb.Core.Interfaces;
using GSMWeb.Core.Helpers;
using GSMWeb.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GSMWeb.Infrastructure.Repositories
{
    public class LocationRepository : Repository<Location>, ILocationRepository
    {
        public LocationRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<(IEnumerable<Location> Locations, int TotalCount)> GetPaginatedAndSearchedAsync(
            PagingParameters pagingParams, string? searchTerm)
        {
            var query = _context.Locations.Include(l => l.User).AsQueryable();

            if (!string.IsNullOrEmpty(searchTerm))
            {
                var lowerCaseSearchTerm = searchTerm.Trim().ToLower();
                query = query.Where(l =>
                    l.LocationName.ToLower().Contains(lowerCaseSearchTerm) ||
                    l.Address.ToLower().Contains(lowerCaseSearchTerm));
            }

            var totalCount = await query.CountAsync();

            var pagedLocations = await query
                .Skip((pagingParams.PageNumber - 1) * pagingParams.PageSize)
                .Take(pagingParams.PageSize)
                .ToListAsync();

            return (pagedLocations, totalCount);
        }
    }
}