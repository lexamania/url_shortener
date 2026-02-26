namespace UrlShortener.Api.Domain.DTOs;

public record ShortUrlDto(
    long Id,
    string Url,
    string ShortUrl)
{
    public bool IsOwner { get; set; }
    public bool CanModify { get; set; }
};
