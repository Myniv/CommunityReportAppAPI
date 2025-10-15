using Application.IRepositories;
using Application.IServices;
using Domain.Models;
using Domain.Models.Dtos;
using Domain.Models.Dtos.Respons;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class DiscussionService : IDiscussionService
    {
        private readonly IDiscussionRepository _discussionRepository;
        public DiscussionService(IDiscussionRepository discussionRepository)
        {
            _discussionRepository = discussionRepository;
        }

        public async Task<IEnumerable<DiscussionResponseDTO>> GetAllDiscussions(int? postId, string? userId)
        {
            var discussions = await _discussionRepository.GetAllFiltered(postId, userId)
                .Select(d => new DiscussionResponseDTO{ 
                    DiscussionId = d.DiscussionId,
                    Message = d.Message,
                    CommunityPostId = d.CommunityPostId,
                    UserId = d.UserId,
                    CreatedAt = d.CreatedAt,
                    UpdatedAt = d.UpdatedAt,
                    DeletedAt = d.DeletedAt
                }).ToListAsync();
            return discussions;
        }

        public async Task<DiscussionResponseDTO?> GetDiscussionById(int id)
        {
            var discussion = await _discussionRepository.GetFirstOrDefaultAsync(d => d.DiscussionId == id, "CommunityPost,User");

            if(discussion == null) return null;

            return new DiscussionResponseDTO{
                DiscussionId = discussion.DiscussionId,
                Message = discussion.Message,
                CommunityPostId = discussion.CommunityPostId,
                UserId = discussion.UserId,
                CommunityPost = new CommunityPostResponseDTO
                {
                    Username = discussion.CommunityPost!.User!.Username,
                    UserPhoto = discussion.CommunityPost!.User!.Photo,
                    UserId = discussion.CommunityPost.UserId,
                    Photo = discussion.CommunityPost.Photo,
                    Title = discussion.CommunityPost.Title,
                    Description = discussion.CommunityPost.Description,
                    Category = discussion.CommunityPost.Category,
                    Status = discussion.CommunityPost.Status,
                    Urgency = discussion.CommunityPost.Urgency,
                    Latitude = discussion.CommunityPost.Latitude,
                    Longitude = discussion.CommunityPost.Longitude,
                    Location = discussion.CommunityPost.Location,
                    CreatedAt = discussion.CreatedAt,
                    UpdatedAt = discussion.UpdatedAt,
                    DeletedAt = discussion.DeletedAt
                },
            };
        }

        public async Task<Discussion> CreateDiscussion(DiscussionDTO discussion)
        {

            var newDiscussion = new Discussion
            {
                CommunityPostId = discussion.CommunityPostId,
                UserId = discussion.UserId,
                Message = discussion.Message,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
                DeletedAt = null,
            };

            await _discussionRepository.AddAsync(newDiscussion);
            await _discussionRepository.SaveAsync();
            return newDiscussion;
        }

        public async Task<bool> UpdateDiscussion(DiscussionDTO discussion, int id)
        {
            var existingDiscussion = await _discussionRepository.GetFirstOrDefaultAsync(d => d.DiscussionId == id);
            if (existingDiscussion == null)
            {
                return false;
            }

            existingDiscussion.Message = discussion.Message;
            existingDiscussion.UpdatedAt = DateTime.UtcNow;

            await _discussionRepository.UpdateAsync(existingDiscussion);
            return true;
        }

        public async Task<bool> DeleteDiscussion(int id)
        {
            var existingDiscussion = await _discussionRepository.GetFirstOrDefaultAsync(d => d.DiscussionId == id);
            if (existingDiscussion == null)
            {
                return false;
            }

            _discussionRepository.Remove(existingDiscussion);
            await _discussionRepository.SaveAsync();
            return true;
        }
    }
}
