using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Models.Entities;

public class Profile
{
    [Key]
    [Column("uid")]
    public string UserId { get; set; }

    [Column("username")]
    public string? Username { get; set; }

    [Column("front_name")]
    public string? FirstName { get; set; }

    [Column("last_name")]
    public string? LastName { get; set; }

    [Column("photo")]
    public string? Photo { get; set; }

    [Column("email")]
    public string Email { get; set; }

    [Column("phone")]
    public string? Phone { get; set; }

    [Column("address")]
    public string? Address { get; set; }

    [Column("role")]
    public string Role { get; set; }

    [Column("created_at")]
    public DateTime? CreatedAt { get; set; }

    [Column("updated_at")]
    public DateTime? UpdatedAt { get; set; }

    [Column("deleted_at")]
    public DateTime? DeletedAt { get; set; }
}