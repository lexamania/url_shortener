using LiteBus.Queries.Abstractions;

using Microsoft.EntityFrameworkCore;

using UrlShortener.Api.Application.Converters;
using UrlShortener.Api.Domain.DTOs;
using UrlShortener.Api.Data;
using UrlShortener.Api.Domain.Exceptions;

namespace UrlShortener.Api.Application.Queries.Urls;

public class UrlDetailsQueryHandler(
    UrlShortenerDbContext dbContext,
    UrlConverter urlConverter
    ) : IQueryHandler<UrlDetailsQuery, DetailedUrlDto>
{
    public async Task<DetailedUrlDto> HandleAsync(UrlDetailsQuery message, CancellationToken cancellationToken = default)
    {
        var url = await dbContext.Urls.FirstOrDefaultAsync(x => x.Id == message.Id)
            ?? throw new StatusException($"Url not found", StatusCodes.Status404NotFound);

        var urlDto = urlConverter.ToDetailedDto(url);
        return urlDto;
    }
}
