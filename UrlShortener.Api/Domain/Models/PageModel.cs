using UrlShortener.Api.Application.Utilities;

namespace UrlShortener.Api.Domain.Models;

public record PageModel(int PageNumber, int PageCount)
{
    public int? TotalPages { get; set; }
    public (int Page, int Count, int Skip) GetNormalized()
        => PaginationUtility.Normalize(PageNumber, PageCount);
}
