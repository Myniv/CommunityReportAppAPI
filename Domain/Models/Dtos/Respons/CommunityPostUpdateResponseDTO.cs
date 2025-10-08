

using Domain.Models.Dtos.Request;

namespace Domain.Models.Dtos.Respons
{
    public class CommunityPostUpdateResponseDTO : CommunityPostUpdateRequestDTO
    {
        public int CommunityPostUpdateId { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public ProfileDiscussionResponseDTO? User { get; set; }
    }
}
