namespace QuizBar.Services.Results.Quizzes;

public class GetQuizServiceResult
{
    public required string Id { get; set; }
    public required string Title { get; set; }
    public string Scope { get; set; }
}