using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Domain.Models.Dtos.Respons
{
    public class ProfileDiscussionResponseDTO
    {
        [JsonPropertyName("username")]
        public string? Username { get; set; }
        [JsonPropertyName("photo")]
        public string? Photo { get; set; }
    }
}
