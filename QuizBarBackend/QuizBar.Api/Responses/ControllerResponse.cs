namespace QuizBar.Api.Responses;

public class ControllerResponse<T> where T : class
{
    public bool Success { get; set; }
    public string Message { get; set; }
    public T Data { get; set; }
}

public class ControllerResponse
{
    public bool Success { get; set; }
    public string Message { get; set; }
}