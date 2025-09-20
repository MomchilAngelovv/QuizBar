namespace QuizBar.Services.Inputs.Teams;

public class GetTeamServiceInput : IServiceInput
{
    public string TeamId { get; set; }

    // IServiceInput
    public string UserId { get; set; }
}