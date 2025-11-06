namespace QuizBar.Shared.ServiceModels.Inputs.QuizLocations;

public class GetSitesServiceInput
{
    public required int Page { get; set; }
    public QuizLocationsFilter Filter { get; set; }
}