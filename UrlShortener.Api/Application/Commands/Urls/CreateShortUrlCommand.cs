using System.ComponentModel.DataAnnotations;

using LiteBus.Commands.Abstractions;

using Newtonsoft.Json;

using UrlShortener.Api.Domain.Attributes;
using UrlShortener.Api.Domain.DTOs;

namespace UrlShortener.Api.Application.Commands.Urls;

public record CreateShortUrlCommand(
    [CustomUrl, Required] string Url,
    string? ShortUrl,
    string? Title
) : ICommand<ShortUrlDto>;
