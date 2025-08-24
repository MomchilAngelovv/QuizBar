namespace QuizBar.Data.Entities;

public class ExceptionLog
{
    // Main
    public required string Id { get; set; }
    public required string Message { get; set; }
    public required string StackTrace { get; set; }
    public string Path { get; set; }
    public string RequestBody { get; set; }
    public string QueryString { get; set; }
    public string RouteValues { get; set; }
    public string Headers { get; set; }
    public required DateTime CreatedOn { get; set; }
}