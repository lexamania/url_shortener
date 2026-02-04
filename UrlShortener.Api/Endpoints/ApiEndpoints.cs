namespace UrlShortener.Api.Endpoints;

public static class ApiEndpoints
{
    public static WebApplication MapEndpoints(this WebApplication app)
    {
        var group = app.MapGroup("/api/v1");
        group.MapUsersEndpoints();
        return app;
    }
}
