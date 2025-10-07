using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Models.Entities;

namespace Domain.Models
{
    public class CommunityPost
    {
        [Key]
        [Column("id")]
        public int PostId { get; set; }

        [Column("user_id")]
        public string UserId { get; set; }
        [ForeignKey(nameof(UserId))]
        public Profile User { get; set; }

        [Column("title")]
        public string Title { get; set; }

        [Column("description")]
        public string Description { get; set; }

        [Column("photo")]
        public string? Photo { get; set; }

        [Column("longitude")]
        public double Longitude { get; set; }

        [Column("latitude")]
        public double Latitude { get; set; }

        [Column("location")]
        public string Location { get; set; }

        [Column("status")]
        public string? Status { get; set; } //Pending, In Progress, Resolved

        [Column("category")]
        public string Category { get; set; }

        [Column("is_report")]
        public bool IsReport { get; set; }

        [Column("urgency")]
        public string Urgency { get; set; } //Low, Medium, High

        [Column("created_at")]
        public DateTime CreatedAt { get; set; }

        [Column("updated_at")]
        public DateTime UpdatedAt { get; set; }

        [Column("deleted_at")]
        public DateTime? DeletedAt { get; set; }
        public ICollection<Discussion> Discussions { get; set; } = new List<Discussion>();

        public ICollection<CommunityPostUpdate> CommunityPostUpdates { get; set; } = new List<CommunityPostUpdate>();
}
}
