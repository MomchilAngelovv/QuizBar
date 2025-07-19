namespace Foksty.Services.Results;

public class ServiceResult<T> where T : class
{
    public required ServiceResultStatus Status { get; set; }
    public string? Message { get; set; }
    public T? Data { get; set; }
}

public enum ServiceResultStatus
{
    Ok = 200,
    Created = 201,
    Forbidden = 403,
    NotFound = 404,
    Conflict = 409,
    InternalServerError = 500,
    ApplictionTimedOut = 504
}