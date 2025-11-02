namespace QuizBar.Services.Results.QuizLocations;

using QuizBar.Shared;

public class GetSitesServiceResult : IPaginatedServiceResult
{
    public required IEnumerable<GetSitesSite> QuizLocations { get; set; }

    // IPaginatedServiceResult
    public required int Page { get; set ; }
    public required int TotalItems { get; set ; }
}

public class GetSitesSite
{
    public required string Id { get; set; }
    public required string Title { get; set; }
    public required Statuses Status { get; set; }
}