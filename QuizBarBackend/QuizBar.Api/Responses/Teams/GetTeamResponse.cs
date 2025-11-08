namespace QuizBar.Api.Responses.Teams;

using QuizBar.Shared;

public class GetTeamResponse
{
    public string Id { get; set; }
    public string Title { get; set; }
    public Statuses Status { get; set; }
}