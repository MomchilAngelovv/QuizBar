namespace QuizBar.Shared.ServiceModels.Results.Teams;

public class GetTeamServiceResult
{
    public string Id { get; set; }
    public string Title { get; set; }
    public Statuses Status { get; set; }
}