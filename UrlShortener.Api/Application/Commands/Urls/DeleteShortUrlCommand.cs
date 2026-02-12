using LiteBus.Commands.Abstractions;

namespace UrlShortener.Api.Application.Commands.Urls;

public record DeleteShortUrlCommand(long[] Ids) : ICommand;
