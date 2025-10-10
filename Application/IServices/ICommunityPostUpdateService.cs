using Domain.Models;
using Domain.Models.Dtos.Request;
using Domain.Models.Dtos.Respons;
using Domain.Models.Entities;

namespace Application.IServices
{
    public interface ICommunityPostUpdateService
    {
        Task<CommunityPostUpdateResponseDTO?> GetCommunityPostUpdateById(int id);
        Task<IEnumerable<CommunityPostUpdate>> GetAllCommunityPostsUpdate(int? postId);
        Task<CommunityPostUpdateResponseDTO?> CreateCommunityPostUpdate(CommunityPostUpdateRequestDTO communityPostUpdate);
        Task<bool> UpdateCommunityPostUpdate(CommunityPostUpdateRequestDTO communityPostUpdate, int id);
        Task<bool> DeleteCommunityPostUpdate(int id);
    }
}
