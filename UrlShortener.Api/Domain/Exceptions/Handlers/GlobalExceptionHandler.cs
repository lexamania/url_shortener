using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace UrlShortener.Api.Domain.Exceptions.Handlers;

public class GlobalExceptionHandler : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
    {
        ProblemDetails problem = exception switch
        {
            StatusException ex => new()
            {
                Title = "Request error",
                Status = ex.StatusCode,
            },
            ArgumentException => new ()
            {
                Title = "Request error",
                Status = StatusCodes.Status400BadRequest,
            },
            KeyNotFoundException => new ()
            {
                Title = "'Not found' error",
                Status = StatusCodes.Status404NotFound,
            },
            _ => new()
            {
                Title = "An unexpected error ocured",
                Status = StatusCodes.Status500InternalServerError,
            }
        };

        problem.Detail = exception.Message;
        problem.Instance = httpContext.Request.Path;
        problem.Extensions["traceId"] = httpContext.TraceIdentifier;

        httpContext.Response.StatusCode = problem.Status!.Value;
        httpContext.Response.ContentType = "application/problem+json";

        await httpContext.Response.WriteAsJsonAsync(problem);
        return true;
    }
}
