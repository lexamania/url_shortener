namespace UrlShortener.Api.Application.Services;

public class AppConfig(IConfiguration configuration)
{
    public string DBConnectionString { get; } = configuration.GetValue<string>("ConnectionStrings:Postgres")!;
    public string DomainUrl { get; } = configuration.GetValue<string>("DomainUrl")!;
}
