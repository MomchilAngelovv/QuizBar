namespace QuizBar.Api.Responses.Quizzes;

public class GetQuizResponse
{
    public GetQuizQuiz Quiz { get; set; }
}

public class GetQuizQuiz
{
    public required string Id { get; set; }
    public required string Title { get; set; }
    public string Scope { get; set; }
}
