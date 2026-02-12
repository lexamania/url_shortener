using LiteBus.Commands.Abstractions;

using Microsoft.EntityFrameworkCore;

using UrlShortener.Api.Application.Converters;
using UrlShortener.Api.Application.DTOs;
using UrlShortener.Api.Data;
using UrlShortener.Api.Exceptions;

namespace UrlShortener.Api.Application.Commands.Urls;

public class UpdateShortUrlCommandHandler(UrlShortenerDbContext dbContext) : ICommandHandler<UpdateShortUrlCommand, DetailedUrlDto>
{
    public async Task<DetailedUrlDto> HandleAsync(UpdateShortUrlCommand message, CancellationToken cancellationToken = default)
    {
        var url = await dbContext.Urls.FirstOrDefaultAsync(x => x.Id == message.Id)
            ?? throw new StatusException($"Url with id '{message.Id}' doesn't found", StatusCodes.Status404NotFound);

        url.Title = message.Title;
        await dbContext.SaveChangesAsync();

        var urlDto = UrlConverter.ToDetailedDto(url);
        return urlDto;
    }
}
