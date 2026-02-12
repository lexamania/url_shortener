using UrlShortener.Api.Application.Interfaces;

namespace UrlShortener.Api.Application.Models;

public record ListResultModel<T>(List<T> Values, PageModel Page) : IPaginable;
