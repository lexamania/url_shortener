using System.Security.Claims;

using UrlShortener.Api.Application.Interfaces;

namespace UrlShortener.Api.Application.Services;

public class UserService : IUserService
{
    public int? UserId { get; }

    public UserService(IHttpContextAccessor httpContextAccessor)
    {
        var user = httpContextAccessor.HttpContext?.User;
        if (user is null) return;

        var strId = user.FindFirstValue(ClaimTypes.NameIdentifier);
        if (int.TryParse(strId, out var userId))
            UserId = userId;
    }
}
