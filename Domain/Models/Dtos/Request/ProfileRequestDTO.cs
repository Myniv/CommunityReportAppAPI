using System.Text.Json.Serialization;

namespace Domain.Models.Dtos.Request
{
    public class ProfileRequestDTO
    {
        [JsonPropertyName("uid")]
        public string? UserId { get; set; }

        [JsonPropertyName("username")]
        public string? Username { get; set; }

        [JsonPropertyName("front_name")]
        public string? FirstName { get; set; }

        [JsonPropertyName("last_name")]
        public string? LastName { get; set; }

        [JsonPropertyName("photo")]
        public string? Photo { get; set; }

        [JsonPropertyName("email")]
        public string? Email { get; set; }

        [JsonPropertyName("phone")]
        public string? Phone { get; set; }

        [JsonPropertyName("address")]
        public string? Address { get; set; }

        [JsonPropertyName("role")]
        public string? Role { get; set; }

        [JsonPropertyName("created_at")]
        public DateTime? CreatedAt { get; set; }

        [JsonPropertyName("updated_at")]
        public DateTime? UpdatedAt { get; set; }

        [JsonPropertyName("deleted_at")]
        public DateTime? DeletedAt { get; set; }
    }
}
