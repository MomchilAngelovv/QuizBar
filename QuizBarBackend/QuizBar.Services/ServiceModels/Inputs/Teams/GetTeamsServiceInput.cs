namespace QuizBar.Services.ServiceModels.Inputs.Teams;

public class GetTeamsServiceInput : IServiceInput
{
    // IServiceInput
    public string UserId { get; set; }

    public int Page { get; set; }
    public TeamsFilter Filter { get; set; }
}