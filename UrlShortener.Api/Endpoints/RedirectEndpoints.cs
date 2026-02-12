using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using UrlShortener.Api.Data;

namespace UrlShortener.Api.Endpoints;

public static class RerouteEndpoints
{
    public static WebApplication MapRedirectEndpoints(this WebApplication app)
    {
        var group = app.MapGroup("/");
        group.MapGet("/{shortUrl}", RedirectByShortUrl);
        return app;
    }

    public static async Task<IResult> RedirectByShortUrl(
        [FromRoute] string shortUrl,
        [FromServices] UrlShortenerDbContext dbContext)
    {
        var url = await dbContext.Urls.FirstOrDefaultAsync(x => x.ShortUrl == shortUrl);
        return url is null
            ? TypedResults.Problem("Url not found", statusCode: StatusCodes.Status404NotFound)
            : Results.Redirect(url.Url);
    }
}
