using LiteBus.Queries.Abstractions;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;

using UrlShortener.Api.Application.Converters;
using UrlShortener.Api.Domain.DTOs;
using UrlShortener.Api.Domain.Models;
using UrlShortener.Api.Application.Utilities;
using UrlShortener.Api.Data;

namespace UrlShortener.Api.Application.Queries.Urls;

public class UrlListQueryHandler(
    UrlShortenerDbContext dbContext,
    IMemoryCache cache,
    UrlConverter urlConverter
    ) : IQueryHandler<UrlListQuery, ListResultModel<ShortUrlDto>>
{
    private const string TotalUrlsCacheKey = "total_urls";

    public async Task<ListResultModel<ShortUrlDto>> HandleAsync(UrlListQuery message, CancellationToken cancellationToken = default)
    {
        var (page, count, skip) = message.Page.GetNormalized();
        var totalPages = PaginationUtility.CalculateTotalPages(count, await GetTotalUrls());
        var pagging = new PageModel(page, count) { TotalPages = totalPages };

        if (page > totalPages)
            return new([], pagging);

        var urls = await dbContext.Urls
            .Skip(skip).Take(count)
            .AsNoTracking()
            .ToListAsync();
        var urlDtoList = urlConverter.ToList(urls, urlConverter.ToShortDto);

        return new(urlDtoList, pagging);
    }

    private async Task<int> GetTotalUrls()
    {
        if (!cache.TryGetValue<int>(TotalUrlsCacheKey, out var result))
        {
            result = await dbContext.Urls.CountAsync();
            var cacheOptions = new MemoryCacheEntryOptions()
                .SetAbsoluteExpiration(TimeSpan.FromMinutes(2))
                .SetSlidingExpiration(TimeSpan.FromMinutes(1));
            cache.Set(TotalUrlsCacheKey, result, cacheOptions);
        }

        return result;
    }
}
