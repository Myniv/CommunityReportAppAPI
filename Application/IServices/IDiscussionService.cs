using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.IServices
{
    public interface IDiscussionService
    {
        Task<Discussion> GetDiscussionById(int id);
        Task<IEnumerable<Discussion>> GetAllDiscussions(int? postId);
        Task<Discussion> CreateDiscussion(Discussion discussion);
        Task<bool> UpdateDiscussion(Discussion discussion, int id);
        Task<bool> DeleteDiscussion(int id);
    }
}
