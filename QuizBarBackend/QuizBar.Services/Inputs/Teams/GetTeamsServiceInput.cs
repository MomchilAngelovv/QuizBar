namespace QuizBar.Services.Inputs.Teams;

public class GetTeamsServiceInput
{
    public required int Page { get; set; }
    public TeamsFilter Filter { get; set; }
}