namespace QuizBar.Api.Responses.Quizzes;

public class GetQuizResponse
{
    public required string Id { get; set; }
    public required string Title { get; set; }
    public string Scope { get; set; }
}