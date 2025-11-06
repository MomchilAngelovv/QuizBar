namespace QuizBar.Shared.ServiceModels.Results.ExceptionLog;

public class GetExceptionLogsServiceResult : IPaginatedServiceResult
{
    public required IEnumerable<GetExceptionLogsException> Exceptions { get; set; }

    // IPaginationServiceResponse
    public required int Page { get; set; }
    public required int TotalItems { get; set; }
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