using Application.IRepositories;
using Application.IServices;
using Domain.Models.Dtos.Request;
using Domain.Models.Dtos.Respons;
using Domain.Models.Entities;

namespace Application.Services
{
    public class CommunityPostUpdateService : ICommunityPostUpdateService
    {
        private readonly ICommunityPostUpdateRepository _communityPostUpdateRepository;
        private readonly ICommunityPostRepository _communityPostRepository;
        public CommunityPostUpdateService(ICommunityPostUpdateRepository communityPostUpdateRepository, ICommunityPostRepository communityPostRepository)
        {
            _communityPostUpdateRepository = communityPostUpdateRepository;
            _communityPostRepository = communityPostRepository;
        }

        public async Task<CommunityPostUpdateResponseDTO?> CreateCommunityPostUpdate(CommunityPostUpdateRequestDTO communityPostUpdate)
        {
            //Todo : 
            // For first create : send email to the users whose region are the same as the user who created the post 
            // For second create : send email to the user who created the post
            var newCommunityPostUpdate = new CommunityPostUpdate
            {
                CommunityPostId = communityPostUpdate.PostId,
                UserId = communityPostUpdate.UserId,
                Title = communityPostUpdate.Title,
                Description = communityPostUpdate.Description,
                Photo = communityPostUpdate.Photo,
                IsResolved = communityPostUpdate.IsResolved ?? false,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
                DeletedAt = null,
            };

            var communityPost = await _communityPostRepository.GetPostById(communityPostUpdate.PostId);

            if (communityPost == null) return null;

            if (communityPost.Status == "pending")
            {
                communityPost.Status = "in_progress";
            }
            else if (newCommunityPostUpdate.IsResolved == true)
            {
                communityPost.Status = "resolved";
            }

            await _communityPostRepository.UpdateAsync(communityPost);

            await _communityPostUpdateRepository.AddAsync(newCommunityPostUpdate);
            await _communityPostUpdateRepository.SaveAsync();

            var response = new CommunityPostUpdateResponseDTO
            {
                Id = newCommunityPostUpdate.CommunityPostProgressId,
                PostId = newCommunityPostUpdate.CommunityPostId,
                UserId = newCommunityPostUpdate.UserId,
                Title = newCommunityPostUpdate.Title,
                Description = newCommunityPostUpdate.Description,
                Photo = newCommunityPostUpdate.Photo,
                IsResolved = newCommunityPostUpdate.IsResolved,
                CreatedAt = newCommunityPostUpdate.CreatedAt,
                UpdatedAt = newCommunityPostUpdate.UpdatedAt,
            };

            return response;
        }

        public async Task<IEnumerable<CommunityPostUpdate>> GetAllCommunityPostsUpdate(int? postId)
        {
            if (postId != null)
            {
                return await _communityPostUpdateRepository.GetAllAsync(c => c.CommunityPostId == postId);
            }
            return await _communityPostUpdateRepository.GetAllAsync();
        }

        public async Task<CommunityPostUpdate> GetCommunityPostUpdateById(int id)
        {
            return await _communityPostUpdateRepository.GetFirstOrDefaultAsync(c => c.CommunityPostProgressId == id);
        }

        public async Task<bool> UpdateCommunityPostUpdate(CommunityPostUpdateRequestDTO communityPostUpdate, int id)
        {
            var existingCommunityPostUpdate = await _communityPostUpdateRepository.GetFirstOrDefaultAsync(c => c.CommunityPostProgressId == id);
            if (existingCommunityPostUpdate == null)
            {
                return false;
            }

            existingCommunityPostUpdate.Title = communityPostUpdate.Title;
            existingCommunityPostUpdate.Description = communityPostUpdate.Description;
            existingCommunityPostUpdate.Photo = communityPostUpdate.Photo;
            existingCommunityPostUpdate.IsResolved = communityPostUpdate.IsResolved ?? false;
            existingCommunityPostUpdate.UpdatedAt = DateTime.UtcNow;

            await _communityPostUpdateRepository.UpdateAsync(existingCommunityPostUpdate);
            return true;
        }

        public async Task<bool> DeleteCommunityPostUpdate(int id)
        {
            var existingCommunityPostUpdate = await _communityPostUpdateRepository.GetFirstOrDefaultAsync(c => c.CommunityPostProgressId == id);
            if (existingCommunityPostUpdate == null)
            {
                return false;
            }

            _communityPostUpdateRepository.Remove(existingCommunityPostUpdate);
            await _communityPostUpdateRepository.SaveAsync();
            return true;
        }
    }
}
