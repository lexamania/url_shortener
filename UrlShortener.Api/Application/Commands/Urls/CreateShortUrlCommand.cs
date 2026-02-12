using System.ComponentModel.DataAnnotations;

using LiteBus.Commands.Abstractions;

using Newtonsoft.Json;

using UrlShortener.Api.Application.Attributes;
using UrlShortener.Api.Application.DTOs;

namespace UrlShortener.Api.Application.Commands.Urls;

public record CreateShortUrlCommand(
    [CustomUrl, Required] string Url,
    string? ShortUrl,
    string? Title
) : ICommand<ShortUrlDto>;
