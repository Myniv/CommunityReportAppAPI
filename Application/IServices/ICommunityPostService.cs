

using Domain.Models;

namespace CommunityReportAppAPI.Application.IServices
{
    public interface ICommunityPostService
    {

        Task<CommunityPostResponseDTO> GetPostById(int id);
        Task<IEnumerable<CommunityPostResponseDTO>> GetAllPosts(string? id, string? status = null, string? category = null, string? location = null, bool? isReport = null, string? urgency = null);
        Task<CommunityPostResponseDTO> CreatePost(CommunityPostRequestDTO post);

        Task<bool> UpdatePost(CommunityPostRequestDTO post, int id);
        Task<bool> DeletePost(int id);
    }
}
