namespace UrlShortener.Api.Application.Utilities;

public static class PaginationUtility
{
    public static (int Page, int Count, int Skip) Normalize(int page, int count)
    {
        var nPage = NormalizePage(page);
        var nCount = NormalizeCount(count);
        var skip = CalculateSkip(page, count);
        return (nPage, nCount, skip);
    }

    public static int NormalizePage(int page)
        => Math.Max(page, 1);

    public static int NormalizeCount(int count, int def = 100)
        => count > 0 ? count : def;

    public static int CalculateSkip(int page, int count)
        => count * (page - 1);

    public static int CalculateTotalPages(int items, int totalItems)
        => (int)MathF.Ceiling((float)totalItems / items);
}
