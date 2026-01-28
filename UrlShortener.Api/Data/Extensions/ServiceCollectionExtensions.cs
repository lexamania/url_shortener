using Microsoft.EntityFrameworkCore;

namespace UrlShortener.Api.Data.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddPostgresDbContext<TContext>(
        this IServiceCollection services,
        string configPath = "ConnectionStrings:Postgres"
        ) where TContext : DbContext
    {
        return services.AddDbContext<TContext>((sp, opts) =>
        {
            var config = sp.GetRequiredService<IConfiguration>();
            var connectionString = config.GetValue<string>(configPath);
            opts.UseNpgsql(connectionString)
                .UseSnakeCaseNamingConvention();
        });
    }
}
