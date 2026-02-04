using System.ComponentModel.DataAnnotations;

using LiteBus.Commands.Abstractions;

using UrlShortener.Api.Application.Attributes;

namespace UrlShortener.Api.Application.Commands.Account;

public record RegisterCommand(
    [EmailValidation, Required, StringLength(255)] string Email,
    [PasswordValidation, Required, StringLength(32, MinimumLength = 8)] string Password
) : ICommand;