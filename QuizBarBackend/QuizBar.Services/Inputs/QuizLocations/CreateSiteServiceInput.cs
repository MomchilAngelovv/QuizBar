namespace QuizBar.Services.Inputs.QuizLocations;

public class CreateSiteServiceInput : IServiceInput
{
    public required string Title { get; set; }

    // IServiceInput
    public required string UserId { get; set; }
}