using LiteBus.Commands.Extensions.MicrosoftDependencyInjection;
using LiteBus.Messaging.Extensions.MicrosoftDependencyInjection;
using LiteBus.Queries.Extensions.MicrosoftDependencyInjection;

using Microsoft.AspNetCore.Authentication.Cookies;

using UrlShortener.Api.Application.Converters;
using UrlShortener.Api.Domain.Interfaces;
using UrlShortener.Api.Application.Services;

namespace UrlShortener.Api.Application.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddCookieAuthentication(this IServiceCollection services)
    {
        services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
            .AddCookie(options =>
            {
               options.LoginPath = "/api/v1/auth/login";
               options.LogoutPath = "/api/v1/auth/logout";
               options.ExpireTimeSpan = TimeSpan.FromDays(7);
               options.SlidingExpiration = true;
               options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
            });
        return services;
    }

    public static IServiceCollection AddApiServices(this IServiceCollection services)
    {
        services.AddScoped<IPasswordManager, PasswordManager>();
        services.AddScoped<IUserService, CurrentUser>();
        services.AddConverters();
        services.AddCQRSServices();

        return services;
    }

    private static IServiceCollection AddConverters(this IServiceCollection services)
        => services.AddScoped<UrlConverter>();

    private static IServiceCollection AddCQRSServices(this IServiceCollection services)
    {
        var assembly = typeof(Program).Assembly;
        return services.AddLiteBus(liteBus =>
        {
            liteBus.AddCommandModule(module => module.RegisterFromAssembly(assembly));
            liteBus.AddQueryModule(module => module.RegisterFromAssembly(assembly));
        });
    }
}
