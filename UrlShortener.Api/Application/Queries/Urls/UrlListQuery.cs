using LiteBus.Queries.Abstractions;

using UrlShortener.Api.Domain.DTOs;
using UrlShortener.Api.Domain.Interfaces;
using UrlShortener.Api.Domain.Models;

namespace UrlShortener.Api.Application.Queries.Urls;

public record UrlListQuery(PageModel Page) : IQuery<ListResultModel<ShortUrlDto>>, IPaginable;