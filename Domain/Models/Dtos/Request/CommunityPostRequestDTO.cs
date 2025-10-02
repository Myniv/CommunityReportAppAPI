using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class CommunityPostRequestDTO
    {
        public string UserId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string? Photo { get; set; }
        public double Longitude { get; set; }
        public double Latitude { get; set; }
        public string Location { get; set; }
        public string? Status { get; set; }
        public string Category { get; set; } 
        public bool IsReport { get; set; }
        public string Urgency { get; set; } //Low, Medium, High
    }
}
