using LiteBus.Commands.Abstractions;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using UrlShortener.Api.Application.Commands.Account;
using UrlShortener.Api.Data;

namespace UrlShortener.Api.Controllers;

[ApiController]
[Route("api/account")]
public class AccountApiController(ICommandMediator commandMediator, UrlShortenerDbContext context) : ControllerBase
{
    [HttpPost("register")]
    public async Task<IActionResult> Register(RegisterCommand command)
    {
        var result = await commandMediator.SendAsync(command);
        return !result.Success 
            ? BadRequest(result.Error) 
            : Ok();
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginCommand command)
    {
        var result = await commandMediator.SendAsync(command);
        return !result.Success 
            ? BadRequest(result.Error) 
            : Ok();
    }

    [HttpPost("logout"), Authorize]
    public async Task<IActionResult> Logout()
    {
        await commandMediator.SendAsync(new LogoutCommand());
        return Ok();
    }

    [HttpGet("users"), Authorize(Roles = "Admin")]
    public async Task<IActionResult> GetUsers()
    {
        var users = await context.Users.ToListAsync();
        return Ok(users);
    }
}
