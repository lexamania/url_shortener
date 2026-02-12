using LiteBus.Commands.Abstractions;

using Microsoft.EntityFrameworkCore;

using UrlShortener.Api.Domain.Interfaces;
using UrlShortener.Api.Application.Utilities;
using UrlShortener.Api.Data;
using UrlShortener.Api.Domain.Exceptions;

namespace UrlShortener.Api.Application.Commands.Urls;

public class DeleteShortUrlCommandHandler(
    UrlShortenerDbContext dbContext,
    IUserService user
    ) : ICommandHandler<DeleteShortUrlCommand>
{
    public async Task HandleAsync(DeleteShortUrlCommand message, CancellationToken cancellationToken = default)
    {
        var filteredUrlIds = dbContext.Urls
            .Where(x => message.Ids.Contains(x.Id))
            .AsNoTracking()
            .AsEnumerable()
            .Where(x => AccessUtility.HasModificationAccess(user, x.UserId))
            .Select(x => x.Id)
            .ToList();

        if (filteredUrlIds.Count == 0)
            throw new StatusException($"You don't have access to these urls", StatusCodes.Status403Forbidden);

        await dbContext.Urls.Where(x => filteredUrlIds.Contains(x.Id)).ExecuteDeleteAsync();
    }
}
