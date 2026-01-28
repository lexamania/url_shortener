using UrlShortener.Api.Data;
using UrlShortener.Api.Data.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Logging.AddSimpleConsole(c => c.SingleLine = true);

builder.Services.AddPostgresDbContext<UrlShortenerDbContext>();

builder.Services.AddControllersWithViews();
builder.Services.AddOpenApiDocument();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    // automatic DB migration
    app.Services.MigrateDatabase<UrlShortenerDbContext>();

    app.UseDeveloperExceptionPage();

    app.UseOpenApi();
    app.UseSwaggerUi();

    app.MapGet("/", context =>
    {
       context.Response.Redirect("/swagger");
       return Task.CompletedTask; 
    });
}
else
{
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();
app.UseAuthorization();

app.MapStaticAssets();
app.MapControllers();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();

app.Run();
