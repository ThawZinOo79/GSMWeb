using GSMWeb.Core.Entities;
using GSMWeb.Core.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Threading.Tasks;

namespace GSMWeb.API.Controllers
{
    [ApiController]
    [Route("api/locations")]
    public class LocationsController : ControllerBase
    {
        private readonly ILocationService _locationService;

        public LocationsController(ILocationService locationService)
        {
            _locationService = locationService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllLocations()
        {
            var locations = await _locationService.GetAllLocationsAsync();
            return Ok(locations);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetLocationById(int id)
        {
            var location = await _locationService.GetLocationByIdAsync(id);
            if (location == null) return NotFound();
            return Ok(location);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> CreateLocation([FromBody] Location location)
        {
            var userId = GetCurrentUserId();
            var createdLocation = await _locationService.CreateLocationAsync(location, userId);
            return CreatedAtAction(nameof(GetLocationById), new { id = createdLocation.Id }, createdLocation);
        }

        [HttpPut("{id}")]
        [Authorize]
        public async Task<IActionResult> UpdateLocation(int id, [FromBody] Location location)
        {
            var userId = GetCurrentUserId();
            var updatedLocation = await _locationService.UpdateLocationAsync(id, location, userId);
            if (updatedLocation == null) return NotFound();
            return Ok(updatedLocation);
        }

        [HttpDelete("{id}")]
        [Authorize]
        public async Task<IActionResult> DeleteLocation(int id)
        {
            var userId = GetCurrentUserId();
            var success = await _locationService.DeleteLocationAsync(id, userId);
            if (!success) return NotFound();
            return NoContent();
        }

        private int GetCurrentUserId()
        {
            var userIdString = User.FindFirstValue(ClaimTypes.NameIdentifier);
            return int.Parse(userIdString!);
        }
    }
}

