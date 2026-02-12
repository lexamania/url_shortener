using System.ComponentModel.DataAnnotations;
using System.Net.Mail;

namespace UrlShortener.Api.Domain.Attributes;

[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter, AllowMultiple = false)]
public sealed class CustomEmailAttribute() : DataTypeAttribute(DataType.EmailAddress)
{
    public override string FormatErrorMessage(string name)
        => "Invalid email format";

    public override bool IsValid(object? value)
    {
        var email = value?.ToString()!.Trim();
        return email is not null
            && MailAddress.TryCreate(email, out var addr)
            && addr.Address.Equals(email);
    }
}