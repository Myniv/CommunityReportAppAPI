using Application.IRepositories;
using CommunityReportAppAPI.Application.IServices;
using Domain.Models.Dtos.Request;
using Domain.Models.Dtos.Response;
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

        public async Task<ProfileResponseDTO> GetProfileById(string id)
        {
            var profile = await _profileRepository.GetProfileById(id);
            return new ProfileResponseDTO
            {
                UserId = profile.UserId,
                Username = profile.Username,
                FirstName = profile.FirstName,
                LastName = profile.LastName,
                Email = profile.Email,
                Phone = profile.Phone,
                Address = profile.Address,
                Photo = profile.Photo,
                Role = profile.Role,
                CreatedAt = profile.CreatedAt,
                UpdatedAt = profile.UpdatedAt,
                DeletedAt = profile.DeletedAt
            };
        }

        public async Task<ProfileResponseDTO> CreateProfile(ProfileRequestDTO profile)
        {
            var result = new Profile
            {
                UserId = profile.UserId,
                Username = profile.Username,
                FirstName = profile.FirstName,
                LastName = profile.LastName,
                Email = profile.Email,
                Phone = profile.Phone,
                Address = profile.Address,
                Photo = profile.Photo,
                Role = profile.Role,
                CreatedAt = profile.CreatedAt?.ToUniversalTime() ?? DateTime.UtcNow,
                UpdatedAt = profile.UpdatedAt?.ToUniversalTime() ?? DateTime.UtcNow,
                DeletedAt = profile.DeletedAt?.ToUniversalTime()
            };

            await _profileRepository.AddAsync(result);
            await _profileRepository.SaveAsync();
            return new ProfileResponseDTO
            {
                UserId = result.UserId,
                Username = result.Username,
                FirstName = result.FirstName,
                LastName = result.LastName,
                Email = result.Email,
                Phone = result.Phone,
                Address = result.Address,
                Photo = result.Photo,
                Role = result.Role,
                CreatedAt = result.CreatedAt,
                UpdatedAt = result.UpdatedAt,
                DeletedAt = result.DeletedAt
            };
        }

        public async Task<bool> UpdateProfile(ProfileRequestDTO profile, string id)
        {
            var existingProfile = await _profileRepository.GetFirstOrDefaultAsync(p => p.UserId == id);
            if (existingProfile == null)
            {
                return false;
            }

            existingProfile.FirstName = profile.FirstName;
            existingProfile.LastName = profile.LastName;
            existingProfile.Email = profile.Email;
            existingProfile.Phone = profile.Phone;
            existingProfile.Address = profile.Address;
            existingProfile.Photo = profile.Photo;
            existingProfile.Username = profile.Username;

            return await _profileRepository.UpdateProfile(existingProfile, id);
        }

        public async Task<bool> DeleteProfile(string id)
        {
            return await _profileRepository.DeleteProfile(id);
        }

        public async Task<IQueryable<ProfileResponseDTO>> GetAllProfiles()
        {
            var profiles = await _profileRepository.GetAllProfiles();
            return profiles.Select(p => new ProfileResponseDTO
            {
                UserId = p.UserId,
                Username = p.Username,
                FirstName = p.FirstName,
                LastName = p.LastName,
                Email = p.Email,
                Phone = p.Phone,
                Address = p.Address,
                Photo = p.Photo,
                Role = p.Role,
                CreatedAt = p.CreatedAt,
                UpdatedAt = p.UpdatedAt,
                DeletedAt = p.DeletedAt
            });
        }

    }
}