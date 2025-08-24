using QuizBar.Shared;

namespace QuizBar.Services.Results.Quizzes;

public class GetQuizzesServiceResult
{
    public required IEnumerable<GetQuizzesQuiz> Quizzes { get; set; }
}

public class GetQuizzesQuiz
{
    public required string Id { get; set; }
    public required string Title { get; set; }
    public string Scope { get; set; }
    public required Status Status { get; set; }
}