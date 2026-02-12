using LiteBus.Commands.Abstractions;
using LiteBus.Queries.Abstractions;

using Microsoft.AspNetCore.Mvc;

using UrlShortener.Api.Application.Commands.Urls;
using UrlShortener.Api.Application.Queries.Urls;

namespace UrlShortener.Api.Endpoints;

public static class EntityEndpoints
{
    public static IEndpointRouteBuilder MapUrlsEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/urls").WithTags("Urls");
        group.MapGet("/", GetUrls);
        group.MapGet("/{urlId}", GetUrlById);
        group.MapPost("/", CreateUrl).RequireAuthorization();
        group.MapPatch("/", UpdateUrl).RequireAuthorization();
        group.MapDelete("/", DeleteUrl).RequireAuthorization();
        return app;
    }

    public static async Task<IResult> GetUrls(
        [FromQuery] int page,
        [FromQuery] int count,
        [FromServices] IQueryMediator mediator)
    {
        var query = new UrlListQuery(new(page, count));
        var result = await mediator.QueryAsync(query);
        return result.Values.Count == 0
            ? TypedResults.NoContent()
            : TypedResults.Ok(result);
    }

    public static async Task<IResult> GetUrlById(
        [FromRoute] long urlId,
        [FromServices] IQueryMediator mediator)
    {
        var query = new UrlDetailsQuery(urlId);
        var result = await mediator.QueryAsync(query);
        return TypedResults.Ok(result);
    }

    public static async Task<IResult> CreateUrl(
        [FromBody] CreateShortUrlCommand command,
        [FromServices] ICommandMediator mediator)
    {
        var result = await mediator.SendAsync(command);
        return TypedResults.Created($"/{result.Id}", result);
    }

    public static async Task<IResult> UpdateUrl(
        [FromBody] UpdateShortUrlCommand command,
        [FromServices] ICommandMediator mediator)
    {
        var result = await mediator.SendAsync(command);
        return TypedResults.Ok(result);
    }

    public static async Task<IResult> DeleteUrl(
        [FromBody] DeleteShortUrlCommand command,
        [FromServices] ICommandMediator mediator)
    {
        await mediator.SendAsync(command);
        return TypedResults.Ok();
    }
}
