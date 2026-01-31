namespace QuizBar.Data.Entities;

public class ExceptionLog
{
    // Main
    public string Id { get; set; }
    public string Message { get; set; }
    public string StackTrace { get; set; }
    public string Path { get; set; }
    public string QueryString { get; set; }
    public string RouteValues { get; set; }
    public string Headers { get; set; }
    public DateTime OccurredOn { get; set; }
}