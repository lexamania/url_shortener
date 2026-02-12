using UrlShortener.Api.Application.Converters.Base;
using UrlShortener.Api.Application.DTOs;
using UrlShortener.Api.Application.Interfaces;
using UrlShortener.Api.Application.Utilities;
using UrlShortener.Api.Data.Entities;

namespace UrlShortener.Api.Application.Converters;

public class UrlConverter(IUserService user) : ConverterBase
{
    public ShortUrlDto ToShortDto(UrlEntity entity)
        => new(entity.Id, entity.Url, entity.ShortUrl!)
        {
            IsCreator = entity.UserId.Equals(user.UserId),
            CanModify = AccessUtility.HasModificationAccess(user, entity.UserId)
        };

    public DetailedUrlDto ToDetailedDto(UrlEntity entity)
        => new(entity.Id, entity.Url, entity.ShortUrl!, entity.Title)
        {
            IsCreator = entity.UserId.Equals(user.UserId),
            CanModify = AccessUtility.HasModificationAccess(user, entity.UserId)
        };
}
