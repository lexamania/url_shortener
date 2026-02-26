namespace UrlShortener.Api.Domain.DTOs;

public record DetailedUrlDto(
    long Id,
    string Url,
    string ShortUrl,
    string? Title)
{
    public bool IsOwner { get; set; }
    public bool CanModify { get; set; }
};