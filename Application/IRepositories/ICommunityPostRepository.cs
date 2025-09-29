using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.IRepositories
{
    public interface ICommunityPostRepository : IRepository<CommunityPost>
    {
        //Task<CommunityPost> GetPostById(int id);
        //Task<CommunityPost> GetAllPost(int? id);
        //Task<CommunityPost> CreatePost(CommunityPost post);
        //Task<bool> UpdatePost(CommunityPost post, int id);
        //Task<bool> DeletePost(int id);
    }
}
