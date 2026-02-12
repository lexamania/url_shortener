using System.ComponentModel;
using System.Security.Claims;

using UrlShortener.Api.Application.Interfaces;
using UrlShortener.Api.Enums;

namespace UrlShortener.Api.Application.Services;

public class CurrentUser : IUserService
{
    public int? UserId { get; }
    public bool IsAdmin { get; }

    public CurrentUser(IHttpContextAccessor httpContextAccessor)
    {
        var user = httpContextAccessor.HttpContext?.User;
        if (user is null) return;

        UserId = GetClaim<int>(user, ClaimTypes.NameIdentifier);
        IsAdmin = GetClaim<string>(user, ClaimTypes.Role)?.Equals(UserRoles.AdminRole) ?? false;
    }

    private T? GetClaim<T>(ClaimsPrincipal user, string claim)
    {
        var str = user.FindFirstValue(claim);
        if (str is null)
            return default;

        if (str is T t)
            return t;
        
        var targetType = typeof(T);
        var converter = TypeDescriptor.GetConverter(targetType);
        var result = converter.ConvertFrom(str);
        
        return result is null
            ? default
            : (T)result;
    }
}
