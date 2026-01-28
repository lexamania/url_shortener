using System.Net.Mail;

namespace UrlShortener.Api.Utilities;

public static class CredentialsUtility
{
    public static bool IsEmailValid(string email)
        => MailAddress.TryCreate(email, out var addr) && addr.Address.Equals(email);

    public static string NormalizeEmail(string email)
        => email.Trim().ToLowerInvariant();

    public static bool ValidateEmail(string email, out string normalizedEmail)
    {
        normalizedEmail = NormalizeEmail(email);
        return IsEmailValid(normalizedEmail);
    }

    public static bool ValidatePassword(string password, out string normalizedPassword)
    {
        normalizedPassword = password.Trim().Replace(" ", "");
        return normalizedPassword.Length >= 8;
    }
}
