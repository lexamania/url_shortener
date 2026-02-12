namespace UrlShortener.Api.Domain.Interfaces;

public interface IUserService
{
    public int? UserId { get; }
    public bool IsAdmin { get; }
}
