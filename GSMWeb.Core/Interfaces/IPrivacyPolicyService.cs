using GSMWeb.Core.Entities;
    using System.Threading.Tasks;

    namespace GSMWeb.Core.Interfaces
    {
        public interface IPrivacyPolicyService
        {
            Task<PrivacyPolicy?> GetPrivacyPolicyAsync();
            Task<PrivacyPolicy> CreateOrUpdatePolicyAsync(PrivacyPolicy policy);
        }
    }