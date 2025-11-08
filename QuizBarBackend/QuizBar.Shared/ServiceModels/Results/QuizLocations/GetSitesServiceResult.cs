namespace QuizBar.Shared.ServiceModels.Results.QuizLocations;

using QuizBar.Shared;
using QuizBar.Shared.ServiceModels.Results;

public class GetSitesServiceResult : IPaginatedServiceResult
{
    // IPaginatedServiceResult
    public required int Page { get; set; }
    public required int TotalItems { get; set; }

    public required IEnumerable<GetSitesSite> QuizLocations { get; set; }
}

public class GetSitesSite
{
    public required string Id { get; set; }
    public required string Title { get; set; }
    public required Statuses Status { get; set; }
}