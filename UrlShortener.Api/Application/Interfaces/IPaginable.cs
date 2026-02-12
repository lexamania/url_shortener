using UrlShortener.Api.Application.Models;

namespace UrlShortener.Api.Application.Interfaces;

public interface IPaginable
{
    public PageModel Page { get; }
}
