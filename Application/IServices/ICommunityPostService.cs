

using Domain.Models;

namespace CommunityReportAppAPI.Application.IServices
{
    public interface ICommunityPostService
    {

        Task<CommunityPost> GetPostById(int id);
        Task<IEnumerable<CommunityPost>> GetAllPosts(string? id, string? status = null, string? category = null, string? location = null, bool? isReport = null, string? urgency = null);
        Task<CommunityPostResponseDTO> CreatePost(CommunityPostDTO post);

        Task<bool> UpdatePost(CommunityPostDTO post, int id);
        Task<bool> DeletePost(int id);
    }
}
