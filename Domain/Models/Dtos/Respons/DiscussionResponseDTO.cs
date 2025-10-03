using Domain.Models.Dtos.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models.Dtos.Respons
{
    public class DiscussionResponseDTO : DiscussionRequestDTO
    {
        public int? DiscussionId { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }
        public ProfileDiscussionResponseDTO? User { get; set; }
    }
}
