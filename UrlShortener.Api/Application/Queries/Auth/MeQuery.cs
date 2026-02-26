using LiteBus.Queries.Abstractions;

using UrlShortener.Api.Domain.DTOs;

namespace UrlShortener.Api.Application.Queries.Auth;

public record MeQuery() : IQuery<UserDto>;
