using Microsoft.EntityFrameworkCore;

using UrlShortener.Api.Application.Services;

namespace UrlShortener.Api.Data.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddPostgresDbContext<TContext>(
        this IServiceCollection services
        ) where TContext : DbContext
    {
        return services.AddDbContext<TContext>((sp, opts) =>
        {
            var config = sp.GetRequiredService<AppConfig>();
            opts.UseNpgsql(config.DBConnectionString)
                .UseSnakeCaseNamingConvention();
        });
    }
}
