namespace QuizBar.Api.Responses.ExceptionLogs;

public class GetExceptionLogResponse
{
    public required string Id { get; set; }
    public required string Message { get; set; }
    public string Path { get; set; }
    public string QueryString { get; set; }
    public string RouteValues { get; set; }
    public string Headers { get; set; }
    public required DateTime OccurredOn { get; set; }
}