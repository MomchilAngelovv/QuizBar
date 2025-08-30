namespace QuizBar.Services.Inputs.ExceptionLogs;

public class CreateExceptionLogServiceInput
{
    public required string Message { get; set; }
    public required string StackTrace { get; set; }
    public string Path { get; set; }
    public string QueryString { get; set; }
    public string RouteValues { get; set; }
    public string Headers { get; set; }
}