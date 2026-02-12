using LiteBus.Commands.Abstractions;

using Microsoft.EntityFrameworkCore;

using UrlShortener.Api.Application.Converters;
using UrlShortener.Api.Application.DTOs;
using UrlShortener.Api.Application.Interfaces;
using UrlShortener.Api.Application.Utilities;
using UrlShortener.Api.Data;
using UrlShortener.Api.Exceptions;

namespace UrlShortener.Api.Application.Commands.Urls;

public class UpdateShortUrlCommandHandler(
    UrlShortenerDbContext dbContext,
    IUserService user,
    UrlConverter urlConverter
    ) : ICommandHandler<UpdateShortUrlCommand, DetailedUrlDto>
{
    public async Task<DetailedUrlDto> HandleAsync(UpdateShortUrlCommand message, CancellationToken cancellationToken = default)
    {
        var url = await dbContext.Urls.FirstOrDefaultAsync(x => x.Id == message.Id)
            ?? throw new StatusException($"Url with id '{message.Id}' doesn't found", StatusCodes.Status404NotFound);

        if (!AccessUtility.HasModificationAccess(user, url.UserId))
            throw new StatusException($"You don't have access to this url", StatusCodes.Status403Forbidden);

        url.Title = message.Title;
        await dbContext.SaveChangesAsync();

        var urlDto = urlConverter.ToDetailedDto(url);
        return urlDto;
    }
}
