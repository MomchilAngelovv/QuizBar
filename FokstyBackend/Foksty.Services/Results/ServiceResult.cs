namespace Foksty.Services.Results;

public class ServiceResult<TServiceResult> where TServiceResult : class
{
    public required bool Success { get; set; }
    public string? Message { get; set; }
    public TServiceResult? Data { get; set; }
}