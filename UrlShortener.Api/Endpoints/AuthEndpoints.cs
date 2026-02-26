using LiteBus.Commands.Abstractions;
using LiteBus.Queries.Abstractions;

using Microsoft.AspNetCore.Mvc;

using UrlShortener.Api.Application.Commands.Auth;
using UrlShortener.Api.Application.Queries.Auth;

namespace UrlShortener.Api.Endpoints;

public static class UsersEndpoints
{
    public static IEndpointRouteBuilder MapAuthEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/auth").WithTags("Auth");
        group.MapGet("/me", Me).RequireAuthorization();
        group.MapPost("/register", Register);
        group.MapPost("/login", Login);
        group.MapPost("/logout", Logout).RequireAuthorization();
        return app;
    }

    public static async Task<IResult> Me([FromServices] IQueryMediator queryMediator)
    {
        var user = await queryMediator.QueryAsync(new MeQuery());
        return TypedResults.Ok(user);
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
