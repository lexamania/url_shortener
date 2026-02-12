using Microsoft.EntityFrameworkCore;

using UrlShortener.Api.Data.Entities;

namespace UrlShortener.Api.Data;

public class UrlShortenerDbContext(DbContextOptions<UrlShortenerDbContext> options) : DbContext(options)
{
    public DbSet<UserEntity> Users { get; set; }
    public DbSet<UrlEntity> Urls { get; set; }

    public async Task UseTransactionAsync(Func<Task> action)
    {
        using var transaction = await Database.BeginTransactionAsync();

        try
        {
            await action();
            await transaction.CommitAsync();
        }
        catch
        {
            await transaction.RollbackAsync();
            throw;
        }
    }
}
