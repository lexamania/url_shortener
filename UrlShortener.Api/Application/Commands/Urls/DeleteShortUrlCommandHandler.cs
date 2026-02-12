using LiteBus.Commands.Abstractions;

using Microsoft.EntityFrameworkCore;

using UrlShortener.Api.Data;

namespace UrlShortener.Api.Application.Commands.Urls;

public class DeleteShortUrlCommandHandler(UrlShortenerDbContext dbContext) : ICommandHandler<DeleteShortUrlCommand>
{
    public async Task HandleAsync(DeleteShortUrlCommand message, CancellationToken cancellationToken = default)
    {
        await dbContext.Urls.Where(x => x.Id == message.Id).ExecuteDeleteAsync();
    }
}
