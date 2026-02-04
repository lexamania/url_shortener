using UrlShortener.Api.Data.Entities;

namespace UrlShortener.Api.Application.Interfaces;

public interface IPasswordManager
{
    Task<string> HashPassword(UserEntity user, string password);
    Task<bool> VerifyPassword(UserEntity user, string password);
}
