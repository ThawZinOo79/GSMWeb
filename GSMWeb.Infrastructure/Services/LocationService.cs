using GSMWeb.Core.Entities;
using GSMWeb.Core.Helpers;
using GSMWeb.Core.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GSMWeb.Infrastructure.Services
{
    public class LocationService : ILocationService
    {
        private readonly ILocationRepository _locationRepository;

        public LocationService(ILocationRepository locationRepository)
        {
            _locationRepository = locationRepository;
        }

        public async Task<Location> CreateLocationAsync(Location location, int userId)
        {
            location.UserId = userId;
            await _locationRepository.AddAsync(location);
            await _locationRepository.SaveChangesAsync();
            return location;
        }

        public async Task<bool> DeleteLocationAsync(int id, int userId)
        {
            var location = await _locationRepository.GetByIdAsync(id);
            if (location == null) return false;

            _locationRepository.Delete(location);
            await _locationRepository.SaveChangesAsync();
            return true;
        }

        public async Task<(IEnumerable<Location> Locations, int TotalCount)> GetPaginatedLocationsAsync(
            PagingParameters pagingParams, string? searchTerm)
        {
            return await _locationRepository.GetPaginatedAndSearchedAsync(pagingParams, searchTerm);
        }

        public async Task<Location?> GetLocationByIdAsync(int id)
        {
            return await _locationRepository.GetByIdAsync(id);
        }

        public async Task<Location?> UpdateLocationAsync(int id, Location updatedLocation, int userId)
        {
            var existingLocation = await _locationRepository.GetByIdAsync(id);
            if (existingLocation == null) return null;

            existingLocation.LocationName = updatedLocation.LocationName;
            existingLocation.Address = updatedLocation.Address;
            existingLocation.MapLink = updatedLocation.MapLink;
            existingLocation.Phone = updatedLocation.Phone;
            existingLocation.PhotoUrl = updatedLocation.PhotoUrl;
            existingLocation.Remark = updatedLocation.Remark;

            _locationRepository.Update(existingLocation);
            await _locationRepository.SaveChangesAsync();
            return existingLocation;
        }
    }
}