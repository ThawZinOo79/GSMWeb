using GSMWeb.Core.Entities;
using GSMWeb.Core.Helpers;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GSMWeb.Core.Interfaces
{
    public interface ILocationService
    {
        Task<(IEnumerable<Location> Locations, int TotalCount)> GetPaginatedLocationsAsync(
            PagingParameters pagingParams, string? searchTerm);
        Task<Location?> GetLocationByIdAsync(int id);
        Task<Location> CreateLocationAsync(Location location, int userId);
        Task<Location?> UpdateLocationAsync(int id, Location location, int userId);
        Task<bool> DeleteLocationAsync(int id, int userId);
    }
}