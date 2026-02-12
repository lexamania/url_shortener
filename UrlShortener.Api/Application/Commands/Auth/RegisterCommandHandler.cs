using LiteBus.Commands.Abstractions;

using Microsoft.EntityFrameworkCore;

using UrlShortener.Api.Domain.Interfaces;
using UrlShortener.Api.Data;
using UrlShortener.Api.Data.Entities;
using UrlShortener.Api.Application.Utilities;
using UrlShortener.Api.Domain.Exceptions;

namespace UrlShortener.Api.Application.Commands.Auth;

public class RegisterCommandHandler(
    UrlShortenerDbContext context,
    IPasswordManager passwordManager
    ) : ICommandHandler<RegisterCommand>
{
    public async Task HandleAsync(RegisterCommand message, CancellationToken cancellationToken = default)
    {
        var password = CredentialsUtility.NormalizePassword(message.Password);
        var email = CredentialsUtility.NormalizeEmail(message.Email);

        var user = await context.Users.SingleOrDefaultAsync(x => x.Email == email);
        if (user is not null)
            throw new StatusException("User already exists", StatusCodes.Status400BadRequest);

        user = new UserEntity()
        {
            Email = email,
            HashPassword = password,
            CreatedOn = DateTime.UtcNow,
        };

        var hashPassword = await passwordManager.HashPassword(user, password);
        var isAdmin = !await context.Users.AnyAsync();

        user.HashPassword = hashPassword;
        user.Admin = isAdmin;

        context.Users.Add(user);
        await context.SaveChangesAsync();
    }
}
