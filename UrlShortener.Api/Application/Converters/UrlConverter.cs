using UrlShortener.Api.Application.Converters.Base;
using UrlShortener.Api.Domain.DTOs;
using UrlShortener.Api.Domain.Interfaces;
using UrlShortener.Api.Application.Utilities;
using UrlShortener.Api.Data.Entities;
using UrlShortener.Api.Application.Services;

namespace UrlShortener.Api.Application.Converters;

public class UrlConverter(IUserService user, AppConfig config) : ConverterBase
{
    public ShortUrlDto ToShortDto(UrlEntity entity)
        => new(entity.Id, entity.Url, ConvertToFullShortUrl(entity.ShortUrl!))
        {
            IsOwner = entity.UserId.Equals(user.UserId),
            CanModify = AccessUtility.HasModificationAccess(user, entity.UserId)
        };

    public DetailedUrlDto ToDetailedDto(UrlEntity entity)
        => new(entity.Id, entity.Url, ConvertToFullShortUrl(entity.ShortUrl!), entity.Title)
        {
            IsOwner = entity.UserId.Equals(user.UserId),
            CanModify = AccessUtility.HasModificationAccess(user, entity.UserId)
        };

    private string ConvertToFullShortUrl(string url)
        => $"{config.DomainUrl}/{url}";
}
