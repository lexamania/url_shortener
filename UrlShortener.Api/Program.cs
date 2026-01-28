using UrlShortener.Api.Application.Extensions;
using UrlShortener.Api.Data;
using UrlShortener.Api.Data.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Logging.ClearProviders();
builder.Logging.AddSimpleConsole(c => c.SingleLine = true);

builder.Services.AddHttpContextAccessor();
builder.Services.AddCookieAuthentication();
builder.Services.AddAuthorization();

builder.Services.AddPostgresDbContext<UrlShortenerDbContext>();
builder.Services.AddCQRSMediator();

builder.Services.AddSwaggerGen();
builder.Services.AddControllersWithViews();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    // Automatic DB migration
    app.Services.MigrateDatabase<UrlShortenerDbContext>();

    app.UseDeveloperExceptionPage();

    app.UseSwagger();
    app.UseSwaggerUI();
    app.MapGet("/", () => Results.LocalRedirect("/swagger"));
}
else
{
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapStaticAssets();
app.MapControllers();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();

app.Run();
