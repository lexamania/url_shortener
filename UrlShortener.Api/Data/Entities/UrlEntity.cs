using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using Microsoft.EntityFrameworkCore;

namespace UrlShortener.Api.Data.Entities;

[Table("urls")]
[Index(nameof(ShortUrl), IsUnique = true)]
public class UrlEntity
{
    [Key]
    public long Id { get; set; }

    [Column(TypeName = "text")]
    public required string Url { get; set; }

    [MaxLength(32)]
    public string? ShortUrl { get; set; }

    [MaxLength(60)]
    public string? Title { get; set; }

    [ForeignKey(nameof(User))]
    public int UserId { get; set; }
    public UserEntity? User { get; }
}
