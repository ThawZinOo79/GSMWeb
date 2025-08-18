using GSMWeb.Core.Entities;
    using GSMWeb.Core.Interfaces;
    using GSMWeb.Infrastructure.Data;
    using Microsoft.EntityFrameworkCore;
    using System.Threading.Tasks;

    namespace GSMWeb.Infrastructure.Repositories
    {
        public class PrivacyPolicyRepository : Repository<PrivacyPolicy>, IPrivacyPolicyRepository
        {
            public PrivacyPolicyRepository(ApplicationDbContext context) : base(context)
            {
            }

            public async Task<PrivacyPolicy?> GetFirstPolicyAsync()
            {
                return await _context.PrivacyPolicies.FirstOrDefaultAsync();
            }
        }
    }