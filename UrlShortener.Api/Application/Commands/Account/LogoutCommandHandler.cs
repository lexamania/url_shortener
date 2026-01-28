using LiteBus.Commands.Abstractions;

using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;

using UrlShortener.Api.Application.DTOs;

namespace UrlShortener.Api.Application.Commands.Account;

public class LogoutCommandHandler(
    IHttpContextAccessor httpContextAccessor
    ): ICommandHandler<LogoutCommand, ResultDTO>
{
    private readonly HttpContext _httpContext = httpContextAccessor.HttpContext!;

    public async Task<ResultDTO> HandleAsync(LogoutCommand message, CancellationToken cancellationToken = default)
    {
        await _httpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        return ResultDTO.Succeed;
    }
}
