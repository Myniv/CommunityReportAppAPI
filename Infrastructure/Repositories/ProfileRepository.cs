using Application.IRepositories;
using Domain.Models.Entities;
using Infrastructure.Context;

namespace Infrastructure.Repositories
{
    public class ProfileRepository : Repository<Profile>, IProfileRepository
    {
        private readonly MyDbContext _db;
        public ProfileRepository(MyDbContext db) : base(db)
        {
            _db = db;
        }

        public async Task<Profile> GetProfileById(string id)
        {
            return await _db.Profiles.FindAsync(id);
        }

        public async Task<IQueryable<Profile>> GetAllProfiles()
        {
            return _db.Profiles.AsQueryable();
        }

        public async Task<Profile> CreateProfile(Profile profile)
        {
            var result = await _db.Profiles.AddAsync(profile);
            await _db.SaveChangesAsync();
            return result.Entity;
        }

        public async Task<bool> UpdateProfile(Profile profile, string id)
        {
            var existingProfile = await _db.Profiles.FindAsync(id);
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
            existingProfile.UpdatedAt = DateTime.UtcNow;

            _db.Profiles.Update(existingProfile);
            await _db.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteProfile(string id)
        {
            var existingProfile = await _db.Profiles.FindAsync(id);
            if (existingProfile == null)
            {
                return false;
            }

            _db.Profiles.Remove(existingProfile);
            await _db.SaveChangesAsync();
            return true;
        }
    }
}