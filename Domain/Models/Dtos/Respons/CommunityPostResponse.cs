using Domain.Models.Dtos.Respons;

namespace Domain.Models
{
    public class CommunityPostResponseDTO
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public string? Username { get; set; }
        public string? UserPhoto { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string? Photo { get; set; }
        public double Longitude { get; set; }
        public double Latitude { get; set; }
        public string Location { get; set; }
        public string? Status { get; set; }
        public string Category { get; set; }
        public bool IsReport { get; set; }
        public string Urgency { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }
        public List<DiscussionResponseDTO>? Discussions { get; set; }
        public List<CommunityPostUpdateResponseDTO>? CommunityPostUpdates { get; set; }
    }
}