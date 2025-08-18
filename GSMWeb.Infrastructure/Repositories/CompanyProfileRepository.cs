using GSMWeb.Core.Entities;
using GSMWeb.Core.Interfaces;
using GSMWeb.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace GSMWeb.Infrastructure.Repositories
{
    public class CompanyProfileRepository : Repository<CompanyProfile>, ICompanyProfileRepository
    {
        public CompanyProfileRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<CompanyProfile?> GetByUserIdAsync(int userId)
        {
            return await _context.CompanyProfiles
                .FirstOrDefaultAsync(p => p.UserId == userId);
        }

        public async Task<CompanyProfile?> GetFirstProfileAsync()
        {
            return await _context.CompanyProfiles.FirstOrDefaultAsync();
        }
    }
}