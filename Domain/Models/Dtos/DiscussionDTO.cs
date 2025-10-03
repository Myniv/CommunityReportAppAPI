using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models.Dtos
{
    public class DiscussionDTO
    {
        public string UserId { get; set; }
        public int CommunityPostId { get; set; }
        public string Message { get; set; }
    }
}
