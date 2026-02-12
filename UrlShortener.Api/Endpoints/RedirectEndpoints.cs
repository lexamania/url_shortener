namespace UrlShortener.Api.Endpoints;

public static class RerouteEndpoints
{
    public static WebApplication MapRedirectEndpoints(this WebApplication app)
    {
        var group = app.MapGroup("/");
        return app;
    }
}
