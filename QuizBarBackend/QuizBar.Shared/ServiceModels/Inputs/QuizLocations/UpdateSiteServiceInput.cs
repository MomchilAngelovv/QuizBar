namespace QuizBar.Shared.ServiceModels.Inputs.QuizLocations;

public class UpdateSiteServiceInput : IServiceInput
{
    public required int QuizLocationId { get; set; }
    public required string Title { get; set; }

    // IServiceInput
    public string UserId { get; set; }
}