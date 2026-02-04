using System.ComponentModel.DataAnnotations;
using UrlShortener.Api.Application.Attributes;

using LiteBus.Commands.Abstractions;

namespace UrlShortener.Api.Application.Commands.Account;

public record LoginCommand(
    [EmailValidation, Required, StringLength(255)] string Email,
    [PasswordValidation, Required, StringLength(32, MinimumLength = 8)] string Password
) : ICommand;
