using GSMWeb.Core.Entities;
using GSMWeb.Core.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace GSMWeb.API.Controllers
{
    [ApiController]
    [Route("api/privacy-policy")]
    public class PrivacyPolicyController : ControllerBase
    {
        private readonly IPrivacyPolicyService _policyService;

        public PrivacyPolicyController(IPrivacyPolicyService policyService)
        {
            _policyService = policyService;
        }

        [HttpGet]
        public async Task<IActionResult> GetPolicy()
        {
            var policy = await _policyService.GetPrivacyPolicyAsync();
            if (policy == null)
            {
                return NotFound("Privacy Policy has not been set yet.");
            }
            return Ok(policy);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> CreateOrUpdatePolicy([FromBody] PrivacyPolicy policy)
        {
            var savedPolicy = await _policyService.CreateOrUpdatePolicyAsync(policy);
            return Ok(savedPolicy);
        }
    }
}