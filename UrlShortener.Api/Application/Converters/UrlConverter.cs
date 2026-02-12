using UrlShortener.Api.Application.DTOs;
using UrlShortener.Api.Data.Entities;

namespace UrlShortener.Api.Application.Converters;

public static class UrlConverter
{
    public static ShortUrlDto ToShortDto(UrlEntity entity)
        => new(entity.Id, entity.Url, entity.ShortUrl!);

    public static DetailedUrlDto ToDetailedDto(UrlEntity entity)
        => new(entity.Id, entity.Url, entity.ShortUrl!, entity.Title);
}
