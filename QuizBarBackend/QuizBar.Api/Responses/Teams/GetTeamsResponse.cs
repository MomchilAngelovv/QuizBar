namespace QuizBar.Api.Responses.Teams;

public class GetTeamsResponse : IPaginatedControllerResponse
{
    // IPaginatedControllerResponse
    public int Page { get; set; }
    public int TotalItems { get; set; }

    public IEnumerable<GetTeamsTeam> Teams { get; set; }
}

public class GetTeamsTeam
{
    public string Id { get; set; }
    public string Title { get; set; }
}