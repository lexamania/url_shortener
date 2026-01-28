using LiteBus.Commands.Abstractions;

using Microsoft.EntityFrameworkCore;

using UrlShortener.Api.Application.DTOs;
using UrlShortener.Api.Application.Interfaces;
using UrlShortener.Api.Data;
using UrlShortener.Api.Data.Entities;
using UrlShortener.Api.Utilities;

namespace UrlShortener.Api.Application.Commands.Account;

public class RegisterCommandHandler(
    UrlShortenerDbContext context,
    IPasswordManager<UserEntity> passwordManager
    ) : ICommandHandler<RegisterCommand, ResultDTO>
{
    public async Task<ResultDTO> HandleAsync(RegisterCommand message, CancellationToken cancellationToken = default)
    {
        if (!CredentialsUtility.ValidateEmail(message.Email, out var email))
            return ResultDTO.FromError("Invalid email format");

        if (!CredentialsUtility.ValidatePassword(message.Password, out var password))
            return ResultDTO.FromError("Password should be at least 8 symbols");

        var user = await context.Users.SingleOrDefaultAsync(x => x.Email == email);
        if (user is not null)
            return ResultDTO.FromError("User already exist");

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

        return ResultDTO.Succeed;
    }
}
