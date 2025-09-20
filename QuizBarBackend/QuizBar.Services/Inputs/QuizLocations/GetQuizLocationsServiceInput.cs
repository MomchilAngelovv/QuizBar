namespace QuizBar.Services.Inputs.QuizLocations;

public class GetQuizLocationsServiceInput
{
    public required int Page { get; set; }
    public QuizLocationsFilter Filter { get; set; }
}