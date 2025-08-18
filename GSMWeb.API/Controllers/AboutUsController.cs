using GSMWeb.API.DTOs;
using GSMWeb.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace GSMWeb.API.Controllers
{
    [ApiController]
    [Route("api/about-us")]
    public class AboutUsController : ControllerBase
    {
        private readonly ICompanyProfileService _profileService;

        public AboutUsController(ICompanyProfileService profileService)
        {
            _profileService = profileService;
        }

        // GET: api/about-us
        [HttpGet]
        public async Task<IActionResult> GetAboutUsData()
        {
            var profile = await _profileService.GetMainProfileAsync();

            if (profile == null)
            {
                return NotFound("About Us information has not been configured yet.");
            }

            var aboutUsData = new AboutUsDto
            {
                CompanyName = profile.CompanyName,
                Description = profile.Description,
                AboutUsText = profile.AboutUs,
                PhotoUrl = profile.PhotoUrl
            };

            return Ok(aboutUsData);
        }
    }
}