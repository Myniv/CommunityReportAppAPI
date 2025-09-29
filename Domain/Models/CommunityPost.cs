using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class CommunityPost
    {
        [Key]
        public int PostId { get; set; }
        public string UserId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Photo { get; set; }
        public double Longitude { get; set; }
        public double Latitude { get; set; }
        public string Address { get; set; }
        public string? Status { get; set; }
        public string Category { get; set; }
        public bool IsReport { get; set; }
        public enum Urgency
        {
            Low,
            Medium,
            High
        }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public DateTime DeletedAt { get; set; }
        public ICollection<Discussion> Discussions { get; set; }
        //public ICollection<Like> Likes { get; set; }
    }
}
