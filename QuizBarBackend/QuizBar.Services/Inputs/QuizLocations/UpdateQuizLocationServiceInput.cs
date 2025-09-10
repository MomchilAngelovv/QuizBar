namespace QuizBar.Services.Inputs.QuizLocations;

public class UpdateQuizLocationServiceInput : IServiceInput
{
    public required int QuizLocationId { get; set; }
    public required string Site { get; set; }

    // IServiceInput
    public string UserId { get; set; }
}