namespace UrlShortener.Api.Domain.Exceptions;

public class StatusException: Exception
{
    public int StatusCode { get; } 

    public StatusException(string message, int statusCode) : base(message)
    {
        StatusCode = statusCode;
    }

    public StatusException(string message) : this(message, StatusCodes.Status400BadRequest)
    {
        
    }
}
