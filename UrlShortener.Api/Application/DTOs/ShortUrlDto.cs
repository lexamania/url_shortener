namespace UrlShortener.Api.Application.DTOs;

public record ShortUrlDto(
    long Id,
    string Url,
    string ShortUrl)
{
    public bool IsCreator { get; set; }
    public bool CanModify { get; set; }
};
