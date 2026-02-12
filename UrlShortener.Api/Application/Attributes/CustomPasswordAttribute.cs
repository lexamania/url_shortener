using System.Buffers;
using System.ComponentModel.DataAnnotations;

namespace UrlShortener.Api.Application.Attributes;

[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter, AllowMultiple = false)]
public sealed class CustomPasswordAttribute() : DataTypeAttribute(DataType.Password)
{
    public override string FormatErrorMessage(string name)
        => "Invalid password. Password should be without spaces";

    public override bool IsValid(object? value)
    {
        var password = value?.ToString()!.Trim();
        return password is { Length: >= 8 }
            && !password.ContainsAny(SearchValues.Create(' '));
    }
}
