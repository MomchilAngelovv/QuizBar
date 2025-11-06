namespace QuizBar.Shared.ServiceModels.Inputs.Teams;

public class CreateTeamServiceInput : IServiceInput
{
    public required string Title { get; set; }

    //IServiceInput
    public required string UserId { get; set; }
}