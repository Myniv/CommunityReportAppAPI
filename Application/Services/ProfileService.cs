using Application.IRepositories;
using CommunityReportAppAPI.Application.IServices;
using Domain.Models.Entities;

namespace Application.Services
{
    public class ProfileService : IProfileService
    {
        private readonly IProfileRepository _profileRepository;

        public ProfileService(IProfileRepository profileRepository)
        {
            _profileRepository = profileRepository;
        }

        public async Task<Profile> GetProfileById(string id)
        {
            return await _profileRepository.GetProfileById(id);
        }

        public async Task<Profile> CreateProfile(Profile profile)
        {
            return await _profileRepository.CreateProfile(profile);
        }

        public async Task<bool> UpdateProfile(Profile profile, string id)
        {
            return await _profileRepository.UpdateProfile(profile, id);
        }

        public async Task<bool> DeleteProfile(string id)
        {
            return await _profileRepository.DeleteProfile(id);
        }

        public async Task<IQueryable<Profile>> GetAllProfiles()
        {
            return await _profileRepository.GetAllProfiles();
        }

    }
}