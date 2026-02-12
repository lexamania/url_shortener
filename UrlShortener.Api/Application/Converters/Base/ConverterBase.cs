namespace UrlShortener.Api.Application.Converters.Base;

public abstract class ConverterBase
{
    public List<TOut> ToList<TIn, TOut>(IEnumerable<TIn> items, Func<TIn, TOut> converter)
        => [.. ToEnumerable(items, converter)];

    public IEnumerable<TOut> ToEnumerable<TIn, TOut>(IEnumerable<TIn> items, Func<TIn, TOut> converter)
    {
        foreach(var item in items)
            yield return converter.Invoke(item);
    }
}
