using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.IRepositories
{
    public interface IDiscussionRepository : IRepository<Discussion>
    {
        //Task<Discussion> GetDiscussionById(int id);
        //Task<Discussion> GetAllDiscussions(int? id);
        //Task<Discussion> CreateDiscussion(Discussion discussion);
        //Task<bool> UpdateDiscussion(Discussion discussion, int id);
        //Task<bool> DeleteDiscussion(int id);
    }
}
