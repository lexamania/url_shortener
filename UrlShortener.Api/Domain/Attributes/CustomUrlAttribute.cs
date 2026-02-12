using System.ComponentModel.DataAnnotations;

namespace UrlShortener.Api.Domain.Attributes;

[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter, AllowMultiple = false)]
public sealed class CustomUrlAttribute() : DataTypeAttribute(DataType.Url)
{
    private readonly string[] _acceptedSchemes = [
        Uri.UriSchemeHttp,
        Uri.UriSchemeHttps
    ];

    public override bool IsValid(object? value)
    {
        var url = value?.ToString()!.Trim();
        return Uri.TryCreate(url, UriKind.Absolute, out var uri)
            && _acceptedSchemes.Contains(uri.Scheme);
    }
}