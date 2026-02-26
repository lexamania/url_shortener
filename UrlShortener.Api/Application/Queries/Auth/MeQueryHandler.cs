using LiteBus.Queries.Abstractions;

using UrlShortener.Api.Domain.DTOs;
using UrlShortener.Api.Domain.Interfaces;

namespace UrlShortener.Api.Application.Queries.Auth;

public class MeQueryHandler(IUserService user) : IQueryHandler<MeQuery, UserDto>
{
    public Task<UserDto> HandleAsync(MeQuery message, CancellationToken cancellationToken = default)
    {
        var result = new UserDto(
            user.UserId!.Value,
            user.Email!,
            user.IsAdmin
        );
        return Task.FromResult(result);
    }
}
