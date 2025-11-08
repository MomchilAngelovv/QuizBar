namespace QuizBar.Shared.ServiceModels.Results.ExceptionLog;

public class GetExceptionLogsServiceResult : IPaginatedServiceResult
{
    // IPaginatedServiceResult
    public int Page { get; set; }
    public int TotalItems { get; set; }

    public IEnumerable<GetExceptionLogsException> Exceptions { get; set; }
}

public class GetExceptionLogsException
{
    public required string Id { get; set; }
    public required string Message { get; set; }
    public string Path { get; set; }
    public string QueryString { get; set; }
    public string RouteValues { get; set; }
    public string Headers { get; set; }
    public required DateTime OccurredOn { get; set; }
}