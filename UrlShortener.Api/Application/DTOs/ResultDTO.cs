namespace UrlShortener.Api.Application.DTOs;

public class ResultDTO<T> : ResultDTO
{
    public T? Value { get; init; }
}

public class ResultDTO
{
    public bool Success { get; init; }
    public string? Error { get; init; }

    public static readonly ResultDTO Succeed = new () { Success = true };

    public static ResultDTO<T> FromError<T>(string error)
        => new()
        {
            Error = error,
            Success = false
        };

    public static ResultDTO FromError(string error)
        => new()
        {
            Error = error,
            Success = false
        };

    public static ResultDTO<T> FromValue<T>(T value)
        => new()
        {
            Value = value,
            Success = true
        };
}
