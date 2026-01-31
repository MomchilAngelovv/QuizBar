using QuizBar.Services.ServiceModels.Inputs;

namespace QuizBar.Services.ServiceModels.Inputs.Teams;

public class UpdateTeamServiceInput : IServiceInput
{
    // IServiceInput
    public string UserId { get; set; }

    public string TeamId { get; set; }
    public string Title { get; set; }
}