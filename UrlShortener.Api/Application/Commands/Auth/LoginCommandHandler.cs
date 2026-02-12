using System.Security.Claims;

using LiteBus.Commands.Abstractions;

using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;

using UrlShortener.Api.Domain.Interfaces;
using UrlShortener.Api.Data;
using UrlShortener.Api.Application.Utilities;
using UrlShortener.Api.Domain.Exceptions;
using UrlShortener.Api.Domain.Enums;

namespace UrlShortener.Api.Application.Commands.Auth;

public class LoginCommandHandler(
    UrlShortenerDbContext context,
    IPasswordManager passwordManager,
    IHttpContextAccessor httpContextAccessor
    ) : ICommandHandler<LoginCommand>
{
    private readonly HttpContext _httpContext = httpContextAccessor.HttpContext!;

    public async Task HandleAsync(LoginCommand message, CancellationToken cancellationToken = default)
    {
        var password = CredentialsUtility.NormalizePassword(message.Password);
        var email = CredentialsUtility.NormalizeEmail(message.Email);

        var user = await context.Users.SingleOrDefaultAsync(x => x.Email == email)
            ?? throw new StatusException("User not found", StatusCodes.Status404NotFound);

        var verificationResult = await passwordManager.VerifyPassword(user, password);
        if (!verificationResult)
            throw new StatusException("Wrong password", StatusCodes.Status400BadRequest);

        // TODO: email verification

        var claims = new List<Claim>
        {
            new(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new(ClaimTypes.Email, user.Email),
            new(ClaimTypes.Role, user.Admin ?  UserRoles.AdminRole : UserRoles.UserRole)
        };

        var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
        await _httpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new(identity));
    }
}