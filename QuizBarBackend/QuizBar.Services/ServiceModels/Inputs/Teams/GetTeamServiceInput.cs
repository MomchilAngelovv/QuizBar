using QuizBar.Services.ServiceModels.Inputs;

namespace QuizBar.Services.ServiceModels.Inputs.Teams;

public class GetTeamServiceInput : IServiceInput
{
    // IServiceInput
    public string UserId { get; set; }

    public string TeamId { get; set; }
}