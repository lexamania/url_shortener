using Microsoft.AspNetCore.Identity;

using UrlShortener.Api.Domain.Interfaces;
using UrlShortener.Api.Data;
using UrlShortener.Api.Data.Entities;

namespace UrlShortener.Api.Application.Services;

public class PasswordManager(UrlShortenerDbContext context) : IPasswordManager
{
    private readonly PasswordHasher<UserEntity> _hasher = new();

    public Task<string> HashPassword(UserEntity user, string password)
    {
        var result = _hasher.HashPassword(user, password);
        return Task.FromResult(result);
    }

    public async Task<bool> VerifyPassword(UserEntity user, string password)
    {
        var result = _hasher.VerifyHashedPassword(user, user.HashPassword, password);
        if (result == PasswordVerificationResult.Failed)
            return false;

        if (result == PasswordVerificationResult.SuccessRehashNeeded)
        {
            user.HashPassword = await HashPassword(user, password);
            await context.SaveChangesAsync();
        }

        return true;
    }
}
