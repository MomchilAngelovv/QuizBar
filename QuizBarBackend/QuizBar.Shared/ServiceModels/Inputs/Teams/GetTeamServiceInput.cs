namespace QuizBar.Shared.ServiceModels.Inputs.Teams;

public class GetTeamServiceInput : IServiceInput
{
    // IServiceInput
    public string UserId { get; set; }

    public string TeamId { get; set; }
}