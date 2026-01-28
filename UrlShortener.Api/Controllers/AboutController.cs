using Microsoft.AspNetCore.Mvc;

namespace UrlShortener.Api.Controllers;

public class AboutController : Controller
{
    public IActionResult Index() => View();
}
