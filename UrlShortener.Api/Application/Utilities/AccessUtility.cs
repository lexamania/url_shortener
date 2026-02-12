using UrlShortener.Api.Application.Interfaces;

namespace UrlShortener.Api.Application.Utilities;

public static class AccessUtility
{
    public static bool HasModificationAccess(IUserService currentUser, int creatorId)
        => currentUser.IsAdmin || creatorId.Equals(currentUser.UserId);
}
