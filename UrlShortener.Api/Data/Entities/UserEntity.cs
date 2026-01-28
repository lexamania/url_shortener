using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UrlShortener.Api.Data.Entities;

[Table("users")]
public class UserEntity
{
    [Key] 
    public int Id { get; set; }

    [MaxLength(255)]
    public required string Email { get; set; }

    [MaxLength(255)]
    public required string Password { get; set; }

    public bool Admin { get; set; }

    [NotMapped] public List<ShortUrlEntity> ShortUrls{ get; } = [];
}
