using UrlShortener.Api.Data;
using UrlShortener.Api.Data.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Logging.AddSimpleConsole(c => c.SingleLine = true);

builder.Services.AddPostgresDbContext<UrlShortenerDbContext>();
builder.Services.AddOpenApiDocument();
builder.Services.AddControllersWithViews();

var app = builder.Build();

app.Services.MigrateDatabase<UrlShortenerDbContext>();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseOpenApi();
    app.UseSwaggerUi();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();
app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();

app.Run();
