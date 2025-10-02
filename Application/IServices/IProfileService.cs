using Domain.Models.Entities;

namespace CommunityReportAppAPI.Application.IServices
{

    public interface IProfileService
    {
        Task<Profile> GetProfileById(string id);
        Task<Profile> CreateProfile(Profile profile);
        Task<bool> UpdateProfile(Profile profile, string id);
        Task<bool> DeleteProfile(string id);
        Task<IQueryable<Profile>> GetAllProfiles();
    }

}