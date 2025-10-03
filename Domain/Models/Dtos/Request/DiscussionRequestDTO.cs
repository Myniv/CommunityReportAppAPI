using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Domain.Models.Dtos.Request
{
    public class DiscussionRequestDTO
    {
        public string? UserId { get; set; }
        public int CommunityPostId { get; set; }
        public string? Message { get; set; }
    }
}
