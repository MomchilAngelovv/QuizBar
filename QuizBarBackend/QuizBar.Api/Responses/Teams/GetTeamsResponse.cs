namespace QuizBar.Api.Responses.Teams;

public class GetTeamsResponse : IPaginationResponse
{
    public required IEnumerable<GetTeamsTeam> Teams { get; set; }
    public required int Page { get; set; }
    public required int TotalItems { get; set; }
}

public class GetTeamsTeam
{
    public required string Id { get; set; }
    public required string Title { get; set; }
}