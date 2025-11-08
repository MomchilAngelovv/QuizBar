namespace QuizBar.Api.Responses.Sites;

using QuizBar.Shared;

public class GetSitesResponse : IPaginationResponse
{
    public required IEnumerable<GetQuizLocationsLocation> QuizLocations { get; set; }

    // IPaginationResponse
    public required int Page { get; set; }
    public required int TotalItems { get; set; }
}

public class GetQuizLocationsLocation
{
    public required string Id { get; set; }
    public required string Title { get; set; }
    public required Statuses Status { get; set; }
}