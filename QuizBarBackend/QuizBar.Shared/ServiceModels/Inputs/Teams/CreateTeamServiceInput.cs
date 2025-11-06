namespace QuizBar.Shared.ServiceModels.Inputs.Teams;

public class CreateTeamServiceInput : IServiceInput
{
    // IServiceInput
    public string UserId { get; set; }

    public string Title { get; set; }
}