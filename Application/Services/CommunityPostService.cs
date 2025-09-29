using Application.IRepositories;
using Domain.Models;
using CommunityReportAppAPI.Application.IServices;
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
        public async Task<IEnumerable<CommunityPost>> GetAllPosts(string? userId)
        {
            if (userId != null)
            {
                return await _communityPostRepository.GetAllAsync(cp => cp.UserId == userId);
            }
            return await _communityPostRepository.GetAllAsync();
        }

        public async Task<CommunityPost> GetPostById(int id)
        {
            return await _communityPostRepository.GetFirstOrDefaultAsync(cp =>cp.PostId == id);
        }

        public async Task<CommunityPost> CreatePost(CommunityPost post)
        {
            await _communityPostRepository.AddAsync(post);
            await _communityPostRepository.SaveAsync();
            return post;
        }

        public async Task<bool> UpdatePost(CommunityPost post, int id)
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
            existingPost.Address = post.Address;
            existingPost.Status = post.Status;
            existingPost.Category = post.Category;
            existingPost.UpdatedAt = DateTime.UtcNow;

            _communityPostRepository.Remove(existingPost);
            await _communityPostRepository.AddAsync(existingPost);
            await _communityPostRepository.SaveAsync();
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

            _communityPostRepository.Remove(existingPost);
            await _communityPostRepository.AddAsync(existingPost);
            await _communityPostRepository.SaveAsync();
            return true;
        }
    }
}
