

using Domain.Models;

namespace CommunityReportAppAPI.Application.IServices
{
    public interface ICommunityPostService
    {
        
        Task<CommunityPost> GetPostById(int id);
        Task<IEnumerable<CommunityPost>> GetAllPosts(string? id);
        Task<CommunityPost> CreatePost(CommunityPost post);
        Task<bool> UpdatePost(CommunityPost post, int id);
        Task<bool> DeletePost(int id);
    }
}
