namespace QuizBar.Services.Inputs.Teams;

public class UpdateTeamServiceInput : IServiceInput
{
    public required string TeamId { get; set; }
    public string Title { get; set; }

    //IServiceInput
    public required string UserId { get; set; }
}