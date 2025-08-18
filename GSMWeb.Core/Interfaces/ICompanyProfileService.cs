using GSMWeb.Core.Entities;
using System.Threading.Tasks;

namespace GSMWeb.Core.Interfaces
{
    public interface ICompanyProfileService
    {
        Task<CompanyProfile?> GetProfileByUserIdAsync(int userId);
        Task<CompanyProfile> CreateProfileAsync(CompanyProfile profile, int userId);
        Task<CompanyProfile?> UpdateProfileAsync(CompanyProfile profile, int userId);
        Task<bool> DeleteProfileAsync(int userId);
        Task<CompanyProfile?> GetMainProfileAsync();
    }
}