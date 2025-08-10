namespace Foksty.Api.Responses;

public class ControllerResponse<T> where T : class
{
    public required bool HasError { get; set; }
    public string? Message { get; set; }
    public required T? Data { get; set; }
}

public class ControllerResult
{
    public required bool HasError { get; set; }
    public string? Message { get; set; }
}