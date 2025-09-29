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
    }
}
