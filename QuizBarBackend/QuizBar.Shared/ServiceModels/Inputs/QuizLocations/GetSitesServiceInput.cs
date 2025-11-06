namespace QuizBar.Shared.ServiceModels.Inputs.QuizLocations;

public class GetSitesServiceInput : IServiceInput
{
    // IServiceInput
    public string UserId { get; set; }

    public int Page { get; set; }
    public QuizLocationsFilter Filter { get; set; }
}