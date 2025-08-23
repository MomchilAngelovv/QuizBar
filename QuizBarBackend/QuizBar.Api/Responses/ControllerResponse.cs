namespace QuizBar.Api.Responses;

public class ControllerResponse<T> where T : class
{
    public required bool Success { get; set; }
    public string? Message { get; set; }
    public required T? Data { get; set; }
}

public class ControllerResponse
{
    public required bool Success { get; set; }
    public string? Message { get; set; }
}