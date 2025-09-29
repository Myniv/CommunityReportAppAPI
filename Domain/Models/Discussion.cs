using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class Discussion
    {
        [Key]
        public int DiscussionId { get; set; }
        [ForeignKey("CommunityPost")]
        public int PostId { get; set; }
        public string UserId { get; set; }
        public string Message { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public DateTime DeletedAt { get; set; }

    }
}
