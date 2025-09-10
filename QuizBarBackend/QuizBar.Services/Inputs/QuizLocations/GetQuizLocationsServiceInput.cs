namespace QuizBar.Services.Inputs.QuizLocations;

public class GetQuizLocationsServiceInput
{
    public int Page { get; set; } = 1;
    public QuizLocationsFilter Filter { get; set; }
}