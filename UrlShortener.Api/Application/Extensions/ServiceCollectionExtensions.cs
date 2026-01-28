using LiteBus.Commands.Extensions.MicrosoftDependencyInjection;
using LiteBus.Messaging.Extensions.MicrosoftDependencyInjection;
using LiteBus.Queries.Extensions.MicrosoftDependencyInjection;

using Microsoft.AspNetCore.Authentication.Cookies;

using UrlShortener.Api.Application.Interfaces;
using UrlShortener.Api.Application.Services;
using UrlShortener.Api.Data.Entities;

namespace UrlShortener.Api.Application.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddCQRSMediator(this IServiceCollection services)
    {
        services.AddScoped<IPasswordManager<UserEntity>, PasswordManager>();

        var assembly = typeof(Program).Assembly;
        return services.AddLiteBus(liteBus =>
        {
            liteBus.AddCommandModule(module => module.RegisterFromAssembly(assembly));
            liteBus.AddQueryModule(module => module.RegisterFromAssembly(assembly));
        });
    }

    public static IServiceCollection AddCookieAuthentication(this IServiceCollection services)
    {
        services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
            .AddCookie(options =>
            {
               options.LoginPath = "/auth/login";
               options.LogoutPath = "/auth/logout";
               options.ExpireTimeSpan = TimeSpan.FromDays(7);
               options.SlidingExpiration = true;
               options.Cookie.SecurePolicy = CookieSecurePolicy.Always; 
            });
        return services;
    }
}
