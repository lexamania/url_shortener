using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UrlShortener.Api.Data.Entities;

[Table("urls")]
public class UrlEntity
{
    [Key, MaxLength(16)]
    public required string Id { get; set; }

    [Column(TypeName = "text")]
    public required string Url { get; set; }

    [MaxLength(60)]
    public string? Title { get; set; }

    [ForeignKey(nameof(User))]
    public int UserId { get; set; }
    public UserEntity? User { get; }
}
