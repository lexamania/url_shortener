using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using Microsoft.EntityFrameworkCore;

namespace UrlShortener.Api.Data.Entities;

[Table("users")]
[Index(nameof(Email), IsUnique = true)]
public class UserEntity
{
    [Key] 
    public int Id { get; set; }

    [MaxLength(255)]
    public required string Email { get; set; }

    [Column(TypeName = "text")]
    public required string HashPassword { get; set; }

    public bool Admin { get; set; }
    public required DateTime CreatedOn { get; set; }

    [NotMapped] public List<UrlEntity> ShortUrls{ get; } = [];
}
