using System.Security.Claims;

using LiteBus.Commands.Abstractions;

using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;

using UrlShortener.Api.Application.DTOs;
using UrlShortener.Api.Application.Interfaces;
using UrlShortener.Api.Data;
using UrlShortener.Api.Data.Entities;
using UrlShortener.Api.Utilities;

namespace UrlShortener.Api.Application.Commands.Account;

public class LoginCommandHandler(
    UrlShortenerDbContext context,
    IPasswordManager<UserEntity> passwordManager,
    IHttpContextAccessor httpContextAccessor
    ) : ICommandHandler<LoginCommand, ResultDTO>
{
    private readonly HttpContext _httpContext = httpContextAccessor.HttpContext!;

    public async Task<ResultDTO> HandleAsync(LoginCommand message, CancellationToken cancellationToken = default)
    {
        if (!CredentialsUtility.ValidateEmail(message.Email, out var email))
            return ResultDTO.FromError("Invalid email format");

        if (!CredentialsUtility.ValidatePassword(message.Password, out var password))
            return ResultDTO.FromError("Wrong password");

        var user = await context.Users.SingleOrDefaultAsync(x => x.Email == email);
        if (user is null)
            return ResultDTO.FromError("User not found");

        var verificationResult = await passwordManager.VerifyPassword(user, password);
        if (!verificationResult)
            return ResultDTO.FromError("Wrong password");

        var claims = new List<Claim>
        {
            new(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new(ClaimTypes.Email, user.Email),
            new(ClaimTypes.Role, user.Admin ? "Admin" : "User")
        };

        var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
        await _httpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new(identity));

        return ResultDTO.Succeed;
    }
}