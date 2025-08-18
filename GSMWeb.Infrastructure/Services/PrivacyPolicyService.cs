using GSMWeb.Core.Entities;
using GSMWeb.Core.Interfaces;
using System.Threading.Tasks;

namespace GSMWeb.Infrastructure.Services
{
    public class PrivacyPolicyService : IPrivacyPolicyService
    {
        private readonly IPrivacyPolicyRepository _policyRepository;

        public PrivacyPolicyService(IPrivacyPolicyRepository policyRepository)
        {
            _policyRepository = policyRepository;
        }

        public async Task<PrivacyPolicy?> GetPrivacyPolicyAsync()
        {
            return await _policyRepository.GetFirstPolicyAsync();
        }

        public async Task<PrivacyPolicy> CreateOrUpdatePolicyAsync(PrivacyPolicy policy)
        {
            var existingPolicy = await _policyRepository.GetFirstPolicyAsync();

            if (existingPolicy == null)
            {
                await _policyRepository.AddAsync(policy);
            }
            else
            {
                existingPolicy.PolicyName1 = policy.PolicyName1;
                existingPolicy.Description1 = policy.Description1;
                existingPolicy.PolicyName2 = policy.PolicyName2;
                existingPolicy.Description2 = policy.Description2;
                existingPolicy.PolicyName3 = policy.PolicyName3;
                existingPolicy.Description3 = policy.Description3;
                existingPolicy.PolicyName4 = policy.PolicyName4;
                existingPolicy.Description4 = policy.Description4;
                existingPolicy.PolicyName5 = policy.PolicyName5;
                existingPolicy.Description5 = policy.Description5;
                _policyRepository.Update(existingPolicy);
            }

            await _policyRepository.SaveChangesAsync();
            return policy;
        }
    }
}