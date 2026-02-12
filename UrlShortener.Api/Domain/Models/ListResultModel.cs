using UrlShortener.Api.Domain.Interfaces;

namespace UrlShortener.Api.Domain.Models;

public record ListResultModel<T>(List<T> Values, PageModel Page) : IPaginable;
