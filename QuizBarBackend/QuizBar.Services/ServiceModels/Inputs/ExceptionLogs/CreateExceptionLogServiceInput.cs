namespace QuizBar.Services.ServiceModels.Inputs.ExceptionLogs;

public class CreateExceptionLogServiceInput
{
    public string Message { get; set; }
    public string StackTrace { get; set; }
    public string Path { get; set; }
    public string QueryString { get; set; }
    public string RouteValues { get; set; }
    public string Headers { get; set; }
}