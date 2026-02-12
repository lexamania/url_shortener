namespace UrlShortener.Api.Application.DTOs;

public record ShortUrlDto(
    long Id,
    string Url,
    string ShortUrl
);
