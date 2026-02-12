using LiteBus.Queries.Abstractions;

using UrlShortener.Api.Application.DTOs;
using UrlShortener.Api.Application.Interfaces;
using UrlShortener.Api.Application.Models;

namespace UrlShortener.Api.Application.Queries.Urls;

public record UrlListQuery(PageModel Page) : IQuery<ListResultModel<ShortUrlDto>>, IPaginable;