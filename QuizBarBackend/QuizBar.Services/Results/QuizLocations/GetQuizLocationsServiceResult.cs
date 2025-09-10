using QuizBar.Shared;

namespace QuizBar.Services.Results.QuizLocations;

public class GetQuizLocationsServiceResult : IPaginatedServiceResult
{
    public required IEnumerable<GetQuizLocationsLocation> QuizLocations { get; set; }

    // IPaginationServiceResponse
    public required int Page { get; set ; }
    public required int TotalItems { get; set ; }
}

public class GetQuizLocationsLocation
{
    public required string Id { get; set; }
    public required string Site { get; set; }
    public required Statuses Status { get; set; }
}