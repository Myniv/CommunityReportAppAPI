using Application.IRepositories;
using Domain.Models;
using Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class DiscussionRepository : Repository<Discussion>, IDiscussionRepository
    {
        private readonly MyDbContext _db;
        public DiscussionRepository(MyDbContext db) : base(db)
        {
            _db = db;
        }

        public IQueryable<Discussion> GetAllFiltered(int? postId, string? userId)
        {
            var query = _db.Discussions.AsQueryable();

            if (!string.IsNullOrEmpty(userId))
                query = query.Where(d => d.UserId == userId);

            if (postId != null)
                query = query.Where(d => d.CommunityPostId == postId);

            query = query.OrderByDescending(d => d.CreatedAt);

            return query;
        }
    }
}
