namespace QuizBar.Shared.ServiceModels.Results.Teams;

public class GetTeamsServiceResult : IPaginatedServiceResult
{
    // IPaginatedServiceResult
    public int Page { get; set; }
    public int TotalItems { get; set; }

    public IEnumerable<GetTeamsTeam> Teams { get; set; }
}

public class GetTeamsTeam
{
    public string Id { get; set; }
    public string Title { get; set; }
    public Statuses Status { get; set; }
}