using Application.IRepositories;
using Domain.Models.Entities;
using Infrastructure.Context;

namespace Infrastructure.Repositories
{
    public class CommunityPostUpdateRepository : Repository<CommunityPostUpdate>, ICommunityPostUpdateRepository
    {
        private readonly MyDbContext _db;
        public CommunityPostUpdateRepository(MyDbContext db) : base(db)
        {
            _db = db;
        }
    }
}
