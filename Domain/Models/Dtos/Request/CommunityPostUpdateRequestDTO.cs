using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models.Dtos.Request
{
    public class CommunityPostUpdateRequestDTO
    {
        public int PostId { get; set; }
        public string UserId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public bool? IsResolved { get; set; }
        public string? Photo { get; set; }
    }
}
