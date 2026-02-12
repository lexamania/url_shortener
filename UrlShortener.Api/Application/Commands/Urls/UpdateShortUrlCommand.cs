using LiteBus.Commands.Abstractions;

using UrlShortener.Api.Application.DTOs;

namespace UrlShortener.Api.Application.Commands.Urls;

public record UpdateShortUrlCommand(
    long Id,
    string Title
) : ICommand<DetailedUrlDto>;
