using System.ComponentModel.DataAnnotations;
using UrlShortener.Api.Domain.Attributes;

using LiteBus.Commands.Abstractions;

namespace UrlShortener.Api.Application.Commands.Auth;

public record LoginCommand(
    [CustomEmail, Required, StringLength(255)] string Email,
    [CustomPassword, Required, StringLength(32, MinimumLength = 8)] string Password
) : ICommand;
