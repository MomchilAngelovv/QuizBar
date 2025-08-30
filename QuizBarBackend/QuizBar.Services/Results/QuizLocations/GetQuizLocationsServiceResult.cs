using QuizBar.Shared;

namespace QuizBar.Services.Results.QuizLocations;

public class GetQuizLocationsServiceResult
{
    public required IEnumerable<GetQuizLocationsLocation> QuizLocations { get; set; }
}

public class GetQuizLocationsLocation
{
    public required string Id { get; set; }
    public required string Site { get; set; }
    public required Statuses Status { get; set; }
}