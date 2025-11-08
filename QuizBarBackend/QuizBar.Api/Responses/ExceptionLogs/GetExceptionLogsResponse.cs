namespace QuizBar.Api.Responses.ExceptionLogs;

public class GetExceptionLogsResponse : IPaginatedControllerResponse
{
    // IPaginatedControllerResponse
    public int Page { get; set; }
    public int TotalItems { get; set; }

    public IEnumerable<GetExceptionLogsException> Exceptions { get; set; }
}

public class GetExceptionLogsException
{
    public string Id { get; set; }
    public string Message { get; set; }
    public string Path { get; set; }
    public string QueryString { get; set; }
    public string RouteValues { get; set; }
    public string Headers { get; set; }
    public DateTime OccurredOn { get; set; }
}