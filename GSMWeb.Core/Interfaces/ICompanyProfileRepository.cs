using GSMWeb.Core.Entities;
using System.Threading.Tasks;

namespace GSMWeb.Core.Interfaces
{
    public interface ICompanyProfileRepository : IRepository<CompanyProfile>
    {
        Task<CompanyProfile?> GetByUserIdAsync(int userId);

        Task<CompanyProfile?> GetFirstProfileAsync();
    }
}