using QuizBar.Shared;

namespace QuizBar.Shared.ServiceModels.Results.Teams;

public class GetTeamServiceResult
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
}