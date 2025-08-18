using GSMWeb.Core.Entities;
    using System.Threading.Tasks;

    namespace GSMWeb.Core.Interfaces
    {
        public interface IPrivacyPolicyRepository : IRepository<PrivacyPolicy>
        {
            Task<PrivacyPolicy?> GetFirstPolicyAsync();
        }
    }