using Domain.Models.Dtos.Request;
using Domain.Models.Entities;

namespace Application.IRepositories;

public interface IProfileRepository : IRepository<Profile>
{
    Task<Profile> GetProfileById(string id);
    Task<Profile> GetProfileLeaderByLocation(string location);
    Task<IQueryable<Profile>> GetAllProfiles();
    Task<Profile> CreateProfile(Profile profile);
    Task<bool> UpdateProfile(Profile profile, string id);
    Task<bool> DeleteProfile(string id);
}