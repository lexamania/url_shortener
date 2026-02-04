namespace UrlShortener.Api.Application.Utilities;

public static class CredentialsUtility
{
    public static string NormalizeEmail(string email)
        => email.Trim().ToLowerInvariant();

    public static string NormalizePassword(string password)
        => password.Trim();
}
