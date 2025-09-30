using Application.IRepositories;
using Domain.Models;
using CommunityReportAppAPI.Application.IServices;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class CommunityPostService : ICommunityPostService
    {
        private readonly ICommunityPostRepository _communityPostRepository;
        public CommunityPostService(ICommunityPostRepository communityPostRepository)
        {
            _communityPostRepository = communityPostRepository;
        }

        public async Task<IEnumerable<CommunityPost>> GetAllPosts(
    string? userId,
    string? status = null,
    string? category = null,
    string? location = null,
    bool? isReport = null,
    string? urgency = null)
        {
            var query = await _communityPostRepository.GetAllPost();

            if (!string.IsNullOrEmpty(userId))
                query = query.Where(cp => cp.UserId == userId);

            if (!string.IsNullOrEmpty(status))
                query = query.Where(cp => cp.Status == status);

            if (!string.IsNullOrEmpty(category))
                query = query.Where(cp => cp.Category == category);

            if (isReport.HasValue)
                query = query.Where(cp => cp.IsReport == isReport.Value);

            if (!string.IsNullOrEmpty(urgency))
                query = query.Where(cp => cp.Urgency == urgency);

            if (!string.IsNullOrEmpty(location))
                query = query.Where(cp => cp.Location == location);

            return await query.ToListAsync();
        }


        public async Task<CommunityPost> GetPostById(int id)
        {
            return await _communityPostRepository.GetFirstOrDefaultAsync(cp => cp.PostId == id);
        }

        public async Task<CommunityPostDTO> CreatePost(CommunityPostDTO post)
        {
            var communityPost = new CommunityPost
            {
                UserId = post.UserId,
                Title = post.Title,
                Description = post.Description,
                Photo = post.Photo,
                Longitude = post.Longitude,
                Latitude = post.Latitude,
                Location = post.Location,
                Status = post.Status,
                Category = post.Category,
                IsReport = post.IsReport,
                Urgency = post.Urgency,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
                DeletedAt = null
            };

            await _communityPostRepository.AddAsync(communityPost);
            await _communityPostRepository.SaveAsync();
            return post;
        }

        public async Task<bool> UpdatePost(CommunityPostDTO post, int id)
        {
            var existingPost = await _communityPostRepository.GetFirstOrDefaultAsync(cp => cp.PostId == id);
            if (existingPost == null)
            {
                return false;
            }

            existingPost.Title = post.Title;
            existingPost.Description = post.Description;
            existingPost.Photo = post.Photo;
            existingPost.Longitude = post.Longitude;
            existingPost.Latitude = post.Latitude;
            existingPost.Location = post.Location;
            existingPost.Status = post.Status;
            existingPost.Category = post.Category;
            existingPost.UpdatedAt = DateTime.UtcNow;
            existingPost.Urgency = post.Urgency;


            await _communityPostRepository.UpdatePost(existingPost, id);
            return true;
        }

        public async Task<bool> DeletePost(int id)
        {
            var existingPost = await _communityPostRepository.GetFirstOrDefaultAsync(cp => cp.PostId == id);
            if (existingPost == null)
            {
                return false;
            }

            existingPost.DeletedAt = DateTime.UtcNow;

            await _communityPostRepository.DeletePost(id);
            return true;
        }
    }
}
