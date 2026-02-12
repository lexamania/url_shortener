namespace UrlShortener.Api.Endpoints;

public static class ApiEndpoints
{
    public static WebApplication MapApiEndpoints(this WebApplication app)
    {
        var group = app.MapGroup("/api/v1").WithTags("Api");
        group.MapUsersEndpoints();
        group.MapUrlsEndpoints();
        return app;
    }
}
