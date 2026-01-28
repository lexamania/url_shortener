using System.ComponentModel.DataAnnotations;

using LiteBus.Commands.Abstractions;

using UrlShortener.Api.Application.DTOs;

namespace UrlShortener.Api.Application.Commands.Account;

public class LoginCommand : ICommand<ResultDTO>
{
    [EmailAddress]
    public required string Email { get; set; }
    public required string Password { get; set; }
}
