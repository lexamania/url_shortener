using LiteBus.Commands.Abstractions;

using Microsoft.EntityFrameworkCore;

using UrlShortener.Api.Application.Converters;
using UrlShortener.Api.Application.DTOs;
using UrlShortener.Api.Application.Interfaces;
using UrlShortener.Api.Application.Utilities;
using UrlShortener.Api.Data;
using UrlShortener.Api.Data.Entities;
using UrlShortener.Api.Exceptions;

namespace UrlShortener.Api.Application.Commands.Urls;

public class CreateShortUrlCommandHandler(
    UrlShortenerDbContext dbContext,
    IUserService userService
    ): ICommandHandler<CreateShortUrlCommand, ShortUrlDto>
{
    public async Task<ShortUrlDto> HandleAsync(CreateShortUrlCommand message, CancellationToken cancellationToken = default)
    {
        if (message.ShortUrl is { Length: > 0 } 
            && await dbContext.Urls.AnyAsync(x => x.ShortUrl == message.ShortUrl))
            throw new StatusException($"Url '{message.ShortUrl}' already exist, choose another one");

        var userId = userService.UserId!.Value;
        var url = new UrlEntity()
        {
            Url = message.Url,
            Title = message.Title,
            UserId = userId
        };

        await dbContext.UseTransactionAsync(async () =>
        {
            dbContext.Urls.Add(url);
            await dbContext.SaveChangesAsync();

            url.ShortUrl = message.ShortUrl ?? RandomUtility.GetRandomHash(url.Id);
            await dbContext.SaveChangesAsync();
        });

        var urlDto = UrlConverter.ToShortDto(url);
        return urlDto;
    }
}