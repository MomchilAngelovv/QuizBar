namespace QuizBar.Shared.ServiceModels.Inputs.Quizzes;

public class CreateQuizServiceInput : IServiceInput
{
    //IServiceInput
    public string UserId { get; set; }

    public string Title { get; set; }
    public string Scope { get; set; }
}