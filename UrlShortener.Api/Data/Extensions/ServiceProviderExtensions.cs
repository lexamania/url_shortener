using Microsoft.EntityFrameworkCore;

namespace UrlShortener.Api.Data.Extensions;

public static class ServiceProviderExtensions
{
    public static IServiceProvider MigrateDatabase<TContext>(this IServiceProvider services)
        where TContext: DbContext
    {
        using var scope = services.CreateScope();
        var db = scope.ServiceProvider.GetRequiredService<TContext>();
        db.Database.Migrate();
        return services;
    }
}
