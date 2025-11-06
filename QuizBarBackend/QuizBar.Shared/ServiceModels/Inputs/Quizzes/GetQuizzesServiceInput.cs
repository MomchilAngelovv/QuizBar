namespace QuizBar.Shared.ServiceModels.Inputs.Quizzes;

public class GetQuizzesServiceInput : IServiceInput
{
    // IServiceInput
    public string UserId { get; set; }

    public int Page { get; set; }
    public QuizzesFilter Filter { get; set; }
}