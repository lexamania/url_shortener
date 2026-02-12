namespace UrlShortener.Api.Application.Converters;

public static class ListConverter
{
    public static List<TOut> ToListDto<TIn, TOut>(IEnumerable<TIn> items, Func<TIn, TOut> converter)
        => [.. ToEnumerableDto(items, converter)];

    public static IEnumerable<TOut> ToEnumerableDto<TIn, TOut>(IEnumerable<TIn> items, Func<TIn, TOut> converter)
    {
        foreach(var item in items)
            yield return converter.Invoke(item);
    }
}
