using LiteBus.Commands.Abstractions;

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using UrlShortener.Api.Application.Commands.Auth;
using UrlShortener.Api.Data;

namespace UrlShortener.Api.Endpoints;

public static class UsersEndpoints
{
    public static IEndpointRouteBuilder MapAuthEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/auth").WithTags("Users");
        group.MapPost("/register", Register);
        group.MapPost("/login", Login);
        group.MapPost("/logout", Logout).RequireAuthorization();
        return app;
    }

    public static async Task<IResult> Register(
        [FromBody] RegisterCommand command,
        [FromServices] ICommandMediator commandMediator)
    {
        await commandMediator.SendAsync(command);
        return TypedResults.Created();
    }

    public static async Task<IResult> Login(
        [FromBody] LoginCommand command,
        [FromServices] ICommandMediator commandMediator)
    {
        await commandMediator.SendAsync(command);
        return TypedResults.NoContent();
    }

    public static async Task<IResult> Logout([FromServices] ICommandMediator commandMediator)
    {
        await commandMediator.SendAsync(new LogoutCommand());
        return TypedResults.NoContent();
    }
}
