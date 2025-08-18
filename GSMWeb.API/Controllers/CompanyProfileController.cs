using GSMWeb.Core.Entities;
using GSMWeb.Core.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Threading.Tasks;
namespace GSMWeb.API.Controllers
{
    [ApiController]
    [Route("api/company-profile")]
    [Authorize]
    public class CompanyProfileController : ControllerBase
    {
        private readonly ICompanyProfileService _profileService;

        public CompanyProfileController(ICompanyProfileService profileService)
        {
            _profileService = profileService;
        }

        [HttpGet]
        public async Task<IActionResult> GetMyProfile()
        {
            var userId = GetCurrentUserId();
            var profile = await _profileService.GetProfileByUserIdAsync(userId);
            if (profile == null)
            {
                return NotFound("No company profile found for this user.");
            }
            return Ok(profile);
        }

        [HttpPost]
        public async Task<IActionResult> CreateMyProfile([FromBody] CompanyProfile profile)
        {
            var userId = GetCurrentUserId();

            // Check if a profile already exists
            var existingProfile = await _profileService.GetProfileByUserIdAsync(userId);
            if (existingProfile != null)
            {
                return Conflict("A company profile already exists for this user.");
            }

            var createdProfile = await _profileService.CreateProfileAsync(profile, userId);
            return CreatedAtAction(nameof(GetMyProfile), createdProfile);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateMyProfile([FromBody] CompanyProfile profile)
        {
            var userId = GetCurrentUserId();
            var updatedProfile = await _profileService.UpdateProfileAsync(profile, userId);

            if (updatedProfile == null)
            {
                return NotFound("No company profile found to update.");
            }

            return Ok(updatedProfile);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteMyProfile()
        {
            var userId = GetCurrentUserId();
            var success = await _profileService.DeleteProfileAsync(userId);

            if (!success)
            {
                return NotFound("No company profile found to delete.");
            }

            return NoContent();
        }
        private int GetCurrentUserId()
        {
            var userIdString = User.FindFirstValue(ClaimTypes.NameIdentifier);
            return int.Parse(userIdString!);
        }
    }
}