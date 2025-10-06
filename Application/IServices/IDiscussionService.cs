using Domain.Models;
using Domain.Models.Dtos;

namespace Application.IServices
{
    public interface IDiscussionService
    {
        Task<Discussion> GetDiscussionById(int id);
        Task<IEnumerable<Discussion>> GetAllDiscussions(int? postId);
        Task<Discussion> CreateDiscussion(DiscussionDTO discussion);
        Task<bool> UpdateDiscussion(DiscussionDTO discussion, int id);
        Task<bool> DeleteDiscussion(int id);
    }
}
