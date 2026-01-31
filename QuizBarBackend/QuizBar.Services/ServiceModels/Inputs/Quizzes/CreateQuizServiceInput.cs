using QuizBar.Services.ServiceModels.Inputs;

namespace QuizBar.Services.ServiceModels.Inputs.Quizzes;

public class CreateQuizServiceInput : IServiceInput
{
    // IServiceInput
    public string UserId { get; set; }

    public string Title { get; set; }
    public string Scope { get; set; }
}