namespace QuizBar.Shared.ServiceModels.Inputs.QuizLocations;

public class CreateSiteServiceInput : IServiceInput
{
    // IServiceInput
    public string UserId { get; set; }

    public string Title { get; set; }
}