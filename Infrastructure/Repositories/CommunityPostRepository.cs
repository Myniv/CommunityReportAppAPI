using Application.IRepositories;
using Domain.Models;
using Infrastructure.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Infrastructure.Repositories
{
    public class CommunityPostRepository : Repository<CommunityPost>, ICommunityPostRepository
    {
        private readonly MyDbContext _db;
        public CommunityPostRepository(MyDbContext db) : base(db)
        {
            _db = db;
        }

        public async Task<CommunityPost> GetPostById(int id)
        {
            return await _db.CommunityPosts.FindAsync(id);
        }

        public async Task<IQueryable<CommunityPost>> GetAllPost()
        {
            return _db.CommunityPosts.AsQueryable();
        }

        public async Task<CommunityPost> CreatePost(CommunityPost post)
        {
            var result = await _db.CommunityPosts.AddAsync(post);
            await _db.SaveChangesAsync();
            return result.Entity;
        }

        public async Task<bool> UpdatePost(CommunityPost post, int id)
        {
            var existingPost = await _db.CommunityPosts.FindAsync(id);
            if (existingPost == null)
            {
                return false;
            }

            existingPost.Title = post.Title;
            existingPost.Description = post.Description;
            existingPost.Category = post.Category;
            existingPost.Urgency = post.Urgency;
            existingPost.Status = post.Status;
            existingPost.IsReport = post.IsReport;

            _db.CommunityPosts.Update(existingPost);
            await _db.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeletePost(int id)
        {
            var existingPost = await _db.CommunityPosts.FindAsync(id);
            if (existingPost == null)
            {
                return false;
            }

            _db.CommunityPosts.Remove(existingPost);
            await _db.SaveChangesAsync();
            return true;
        }


    }
}
