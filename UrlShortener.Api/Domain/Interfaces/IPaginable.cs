using UrlShortener.Api.Domain.Models;

namespace UrlShortener.Api.Domain.Interfaces;

public interface IPaginable
{
    public PageModel Page { get; }
}
