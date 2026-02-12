using LiteBus.Queries.Abstractions;

using UrlShortener.Api.Application.DTOs;

namespace UrlShortener.Api.Application.Queries.Urls;

public record UrlDetailsQuery(long Id) : IQuery<DetailedUrlDto>;
