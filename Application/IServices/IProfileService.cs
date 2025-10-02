using Domain.Models.Dtos.Request;
using Domain.Models.Dtos.Response;
using Domain.Models.Entities;

namespace CommunityReportAppAPI.Application.IServices
{

    public interface IProfileService
    {
        Task<ProfileResponseDTO> GetProfileById(string id);
        Task<ProfileResponseDTO> CreateProfile(ProfileRequestDTO profile);
        Task<bool> UpdateProfile(ProfileRequestDTO profile, string id);
        Task<bool> DeleteProfile(string id);
        Task<IQueryable<ProfileResponseDTO>> GetAllProfiles();
    }

}