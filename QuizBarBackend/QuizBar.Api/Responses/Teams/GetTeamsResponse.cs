namespace QuizBar.Api.Responses.Teams;

public class GetTeamsResponse : IPaginatedControllerResponse
{
    // IPaginatedControllerResponse
    public int Page { get; set; }
    public int TotalItems { get; set; }

    public IEnumerable<Team> Teams { get; set; }
}

public class Team
{
    public string Id { get; set; }
    public string Title { get; set; }
}