using QuizBar.Shared;

namespace QuizBar.Services.Results.Teams;

public class GetTeamsServiceResult : IPaginatedServiceResult
{
    public required IEnumerable<GetTeamsTeam> Teams { get; set; }

    // IPaginationServiceResponse
    public required int Page { get; set; }
    public required int TotalItems { get; set; }
}

public class GetTeamsTeam
{
    public required string Id { get; set; }
    public required string Title { get; set; }
    public required Statuses Status { get; set; }
}