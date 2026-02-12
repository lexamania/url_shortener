using UrlShortener.Api.Endpoints;
using UrlShortener.Api.Application.Extensions;
using UrlShortener.Api.Data;
using UrlShortener.Api.Data.Extensions;
using UrlShortener.Api.Domain.Exceptions.Handlers;

var builder = WebApplication.CreateBuilder(args);

// Add logger
builder.Logging.ClearProviders();
builder.Logging.AddConsole();
builder.Logging.SetMinimumLevel(LogLevel.Information);

// Add authorization
builder.Services.AddHttpContextAccessor();
builder.Services.AddCookieAuthentication();
builder.Services.AddAuthorization();

// Add main services
builder.Services.AddPostgresDbContext<UrlShortenerDbContext>();
builder.Services.AddApiServices();
builder.Services.AddMemoryCache();

// Add validation
builder.Services.AddValidation();
builder.Services.AddExceptionHandler<GlobalExceptionHandler>();

// Add api
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Enable Problem Details
builder.Services.AddProblemDetails();

// Build app
var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    // Automatic DB migration
    app.Services.MigrateDatabase<UrlShortenerDbContext>();

    // Use swagger page
    app.MapGet("/", () => Results.LocalRedirect("/swagger")).ExcludeFromDescription();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseExceptionHandler();

// Use authorization
app.UseAuthentication();
app.UseAuthorization();

// MapEndpoints
app.MapApiEndpoints();
app.MapRedirectEndpoints();

app.Run();
