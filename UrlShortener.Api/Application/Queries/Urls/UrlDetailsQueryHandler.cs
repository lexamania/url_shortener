using LiteBus.Queries.Abstractions;

using Microsoft.EntityFrameworkCore;

using UrlShortener.Api.Application.Converters;
using UrlShortener.Api.Application.DTOs;
using UrlShortener.Api.Data;
using UrlShortener.Api.Exceptions;

namespace UrlShortener.Api.Application.Queries.Urls;

public class UrlDetailsQueryHandler(UrlShortenerDbContext dbContext) : IQueryHandler<UrlDetailsQuery, DetailedUrlDto>
{
    public async Task<DetailedUrlDto> HandleAsync(UrlDetailsQuery message, CancellationToken cancellationToken = default)
    {
        var url = await dbContext.Urls.FirstOrDefaultAsync(x => x.Id == message.Id)
            ?? throw new StatusException($"Url not found", StatusCodes.Status404NotFound);

        var urlDto = UrlConverter.ToDetailedDto(url);
        return urlDto;
    }
}
