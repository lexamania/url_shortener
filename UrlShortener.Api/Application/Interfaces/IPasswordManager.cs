namespace UrlShortener.Api.Application.Interfaces;

public interface IPasswordManager<TUser> where TUser : class
{
    Task<string> HashPassword(TUser user, string password);
    Task<bool> VerifyPassword(TUser user, string password);
}
