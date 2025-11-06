namespace QuizBar.Shared.ServiceModels.Inputs.Quizzes;

public class GetQuizzesServiceInput
{
    public required int Page { get; set; }
    public QuizzesFilter Filter { get; set; }
}