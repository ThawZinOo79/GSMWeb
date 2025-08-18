using GSMWeb.API.DTOs;
using GSMWeb.Core.Entities;
using GSMWeb.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GSMWeb.API.Controllers
{
    [ApiController]
    [Route("api/contact-us")]
    public class ContactUsController : ControllerBase
    {
        private readonly ICompanyProfileService _profileService;

        public ContactUsController(ICompanyProfileService profileService)
        {
            _profileService = profileService;
        }

        [HttpGet]
        public async Task<IActionResult> GetContactUsData()
        {
            var profile = await _profileService.GetMainProfileAsync();

            if (profile == null)
            {
                return NotFound("Contact information has not been configured yet.");
            }

            var contactUsData = MapToContactUsDto(profile);

            return Ok(contactUsData);
        }

        private ContactUsDto MapToContactUsDto(CompanyProfile profile)
        {
            var dto = new ContactUsDto
            {
                MainAddress = $"{profile.HoAddress}, {profile.Address1}".TrimEnd(new char[] { ' ', ',' }),

                PhoneNumbers = new List<string>(),

                EmailAddresses = new List<string>(),

                SocialMediaLinks = new Dictionary<string, string?>()
            };

            if (!string.IsNullOrWhiteSpace(profile.HotlinePh)) dto.PhoneNumbers.Add(profile.HotlinePh);
            if (!string.IsNullOrWhiteSpace(profile.Phone1)) dto.PhoneNumbers.Add(profile.Phone1);
            if (!string.IsNullOrWhiteSpace(profile.Phone2)) dto.PhoneNumbers.Add(profile.Phone2);

            if (!string.IsNullOrWhiteSpace(profile.Email)) dto.EmailAddresses.Add(profile.Email);
            if (!string.IsNullOrWhiteSpace(profile.InfoMail)) dto.EmailAddresses.Add(profile.InfoMail);
            if (!string.IsNullOrWhiteSpace(profile.HrMail)) dto.EmailAddresses.Add(profile.HrMail);

            if (!string.IsNullOrWhiteSpace(profile.FacebookLink)) dto.SocialMediaLinks["Facebook"] = profile.FacebookLink;
            if (!string.IsNullOrWhiteSpace(profile.InstagramLink)) dto.SocialMediaLinks["Instagram"] = profile.InstagramLink;
            if (!string.IsNullOrWhiteSpace(profile.YoutubeLink)) dto.SocialMediaLinks["Youtube"] = profile.YoutubeLink;
            if (!string.IsNullOrWhiteSpace(profile.TiktokLink)) dto.SocialMediaLinks["Tiktok"] = profile.TiktokLink;

            return dto;
        }
    }
}