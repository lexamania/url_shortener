using Microsoft.EntityFrameworkCore;

using UrlShortener.Api.Data.Entities;

namespace UrlShortener.Api.Data;

public class UrlShortenerDbContext(DbContextOptions<UrlShortenerDbContext> options) : DbContext(options)
{
    public DbSet<UserEntity> Users { get; set; }
    public DbSet<UrlEntity> Urls { get; set; }
}
