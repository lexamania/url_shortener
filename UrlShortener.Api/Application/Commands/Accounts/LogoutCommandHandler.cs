using LiteBus.Commands.Abstractions;

using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace UrlShortener.Api.Application.Commands.Accounts;

public class LogoutCommandHandler(
    IHttpContextAccessor httpContextAccessor
    ): ICommandHandler<LogoutCommand>
{
    private readonly HttpContext _httpContext = httpContextAccessor.HttpContext!;

    public async Task HandleAsync(LogoutCommand message, CancellationToken cancellationToken = default)
    {
        await _httpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
    }
}
