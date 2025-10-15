using Domain.Models;
using Domain.Models.Dtos;
using Domain.Models.Dtos.Respons;

namespace Application.IServices
{
    public interface IDiscussionService
    {
        Task<DiscussionResponseDTO?> GetDiscussionById(int id);
        Task<IEnumerable<DiscussionResponseDTO>> GetAllDiscussions(int? postId, string? userId);
        Task<Discussion> CreateDiscussion(DiscussionDTO discussion);
        Task<bool> UpdateDiscussion(DiscussionDTO discussion, int id);
        Task<bool> DeleteDiscussion(int id);
    }
}
