using Application.IRepositories;
using Application.IServices;
using Domain.Models;
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

        public async Task<IEnumerable<Discussion>> GetAllDiscussions(int? postId)
        {
            if (postId != null)
            {
                return await _discussionRepository.GetAllAsync(d => d.PostId == postId);
            }
            return await _discussionRepository.GetAllAsync();
            
        }

        public async Task<Discussion> GetDiscussionById(int id)
        {
            return await _discussionRepository.GetFirstOrDefaultAsync(d => d.DiscussionId == id);
        }

        public async Task<Discussion> CreateDiscussion(Discussion discussion)
        {
            await _discussionRepository.AddAsync(discussion);
            await _discussionRepository.SaveAsync();
            return discussion;
        }

        public async Task<bool> UpdateDiscussion(Discussion discussion, int id)
        {
            var existingDiscussion = await _discussionRepository.GetFirstOrDefaultAsync(d => d.DiscussionId == id);
            if (existingDiscussion == null)
            {
                return false;
            }

            existingDiscussion.Message = discussion.Message;
            existingDiscussion.UpdatedAt = DateTime.UtcNow;

            _discussionRepository.Remove(existingDiscussion);
            await _discussionRepository.AddAsync(existingDiscussion);
            await _discussionRepository.SaveAsync();
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
