using GSMWeb.Core.Entities;
using GSMWeb.Core.Interfaces;
using System.Threading.Tasks;

namespace GSMWeb.Infrastructure.Services
{
    public class CompanyProfileService : ICompanyProfileService
    {
        private readonly ICompanyProfileRepository _profileRepository;

        public CompanyProfileService(ICompanyProfileRepository profileRepository)
        {
            _profileRepository = profileRepository;
        }

        public async Task<CompanyProfile> CreateProfileAsync(CompanyProfile profile, int userId)
        {
            profile.UserId = userId;
            await _profileRepository.AddAsync(profile);
            await _profileRepository.SaveChangesAsync();
            return profile;
        }

        public async Task<CompanyProfile?> GetProfileByUserIdAsync(int userId)
        {
            return await _profileRepository.GetByUserIdAsync(userId);
        }

        public async Task<CompanyProfile?> UpdateProfileAsync(CompanyProfile updatedProfile, int userId)
        {
            var existingProfile = await _profileRepository.GetByUserIdAsync(userId);
            if (existingProfile == null)
            {
                return null;
            }

            existingProfile.CompanyName = updatedProfile.CompanyName;
            existingProfile.Description = updatedProfile.Description;
            existingProfile.HoAddress = updatedProfile.HoAddress;
            existingProfile.Address1 = updatedProfile.Address1;
            existingProfile.Address2 = updatedProfile.Address2;
            existingProfile.FacebookLink = updatedProfile.FacebookLink;
            existingProfile.Email = updatedProfile.Email;
            existingProfile.PhotoUrl = updatedProfile.PhotoUrl;
            existingProfile.AboutUs = updatedProfile.AboutUs;
            existingProfile.PublishDate = updatedProfile.PublishDate;

            _profileRepository.Update(existingProfile);
            await _profileRepository.SaveChangesAsync();
            return existingProfile;
        }

        public async Task<bool> DeleteProfileAsync(int userId)
        {
            var profile = await _profileRepository.GetByUserIdAsync(userId);
            if (profile == null) return false;

            _profileRepository.Delete(profile);
            await _profileRepository.SaveChangesAsync();
            return true;
        }

        public async Task<CompanyProfile?> GetMainProfileAsync()
        {
            return await _profileRepository.GetFirstProfileAsync();
        }
    }
}