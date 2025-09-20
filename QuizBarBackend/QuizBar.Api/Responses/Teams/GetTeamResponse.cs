using QuizBar.Services.Results.Teams;
using QuizBar.Shared;

namespace QuizBar.Api.Responses.Teams;

public class GetTeamResponse
{
    public required string Id { get; set; }
    public required string Title { get; set; }
    public required Statuses Status { get; set; }

    public required IEnumerable<GetTeamResultServiceResult> Results { get; set; }
}

public class GetTeamResultServiceResult
{
    public required double Score { get; set; }
    public required string TeamId { get; set; }
    public required string QuizEventId { get; set; }
}