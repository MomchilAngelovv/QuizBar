namespace QuizBar.Shared.ServiceModels.Inputs.Quizzes;

public class CreateQuizServiceInput : IServiceInput
{
    public required string Title { get; set; }
    public string? Scope { get; set; }

    //IServiceInput
    public required string UserId { get; set; }
}