using System.ComponentModel.DataAnnotations;

using LiteBus.Commands.Abstractions;

using UrlShortener.Api.Application.Attributes;

namespace UrlShortener.Api.Application.Commands.Auth;

public record RegisterCommand(
    [CustomEmail, Required, StringLength(255)] string Email,
    [CustomPassword, Required, StringLength(32, MinimumLength = 8)] string Password
) : ICommand;