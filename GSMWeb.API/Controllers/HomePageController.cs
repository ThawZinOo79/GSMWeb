using GSMWeb.API.DTOs;
using GSMWeb.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace GSMWeb.API.Controllers
{
    [ApiController]
    [Route("api/home-page")]
    public class HomePageController : ControllerBase
    {
        private readonly ICompanyProfileService _profileService;

        public HomePageController(ICompanyProfileService profileService)
        {
            _profileService = profileService;
        }

        [HttpGet]
        public async Task<IActionResult> GetHomePageData()
        {
            var profile = await _profileService.GetMainProfileAsync();

            if (profile == null)
            {
                return NotFound("Home page data has not been configured yet.");
            }

            var homePageData = new HomePageDto
            {
                Description = profile.Description,
                ImageUrl = profile.PhotoUrl
            };

            return Ok(homePageData);
        }
    }
}