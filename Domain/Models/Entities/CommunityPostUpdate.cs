using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Models.Entities
{
    public class CommunityPostUpdate
    {
        [Key]
        [Column("id")]
        public int CommunityPostUpdateId { get; set; }

        [Column("post_id")]
        public int CommunityPostId { get; set; }
        
        [Column("user_id")]
        public string UserId { get; set; }
        
        [Column("title")]
        public string Title { get; set; }

        [Column("description")]
        public string Description { get; set; }

        [Column("photo")]
        public string? Photo { get; set; }
        
        [Column("is_resolved")]
        public bool IsResolved { get; set; }

        [Column("created_at")]
        public DateTime CreatedAt { get; set; }

        [Column("updated_at")]
        public DateTime UpdatedAt { get; set; }

        [Column("deleted_at")]
        public DateTime? DeletedAt { get; set; }

        [ForeignKey(nameof(CommunityPostId))]
        public virtual CommunityPost? CommunityPost { get; set; }

        [ForeignKey(nameof(UserId))]
        public Profile? User { get; set; }
    }
}
