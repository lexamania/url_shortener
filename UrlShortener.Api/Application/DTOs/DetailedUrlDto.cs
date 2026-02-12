namespace UrlShortener.Api.Application.DTOs;

public record DetailedUrlDto(
    long Id,
    string Url,
    string ShortUrl,
    string? Title
);