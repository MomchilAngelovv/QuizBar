namespace QuizBar.Shared.ServiceModels.Results.Teams;

public class GetTeamServiceResult
{
    public string Id { get; set; }
    public string Title { get; set; }
    public Statuses Status { get; set; }

    public IEnumerable<GetTeamResultServiceResult> Results { get; set; }
}

public class GetTeamResultServiceResult
{
    public double Score { get; set; }
    public string TeamId { get; set; }
}