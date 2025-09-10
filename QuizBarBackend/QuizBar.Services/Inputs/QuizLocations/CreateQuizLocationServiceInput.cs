namespace QuizBar.Services.Inputs.QuizLocations;

public class CreateQuizLocationServiceInput : IServiceInput
{
    public required string Site { get; set; }

    // IServiceInput
    public required string UserId { get; set; }
}
