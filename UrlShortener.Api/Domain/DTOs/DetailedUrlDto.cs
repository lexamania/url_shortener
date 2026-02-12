namespace UrlShortener.Api.Domain.DTOs;

public record DetailedUrlDto(
    long Id,
    string Url,
    string ShortUrl,
    string? Title)
{
    public bool IsCreator { get; set; }
    public bool CanModify { get; set; }
};