using Domain.Models.Entities;
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
        [Column("id")]
        public int DiscussionId { get; set; }
        [Column("post_id")]
        public int CommunityPostId { get; set; }

        [Column("user_id")]
        public string UserId { get; set; }

        [Column("message")]
        public string Message { get; set; }

        [Column("created_at")]
        public DateTime CreatedAt { get; set; }

        [Column("updated_at")]
        public DateTime UpdatedAt { get; set; }

        [Column("deleted_at")]
        public DateTime? DeletedAt { get; set; }
        public virtual Profile? User { get; set; }
        [ForeignKey(nameof(CommunityPostId))]
        public virtual CommunityPost? CommunityPost { get; set; }
    }
}
