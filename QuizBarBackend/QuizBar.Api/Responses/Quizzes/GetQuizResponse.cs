namespace QuizBar.Api.Responses.Quizzes;

public class GetQuizResponse
{
    public required GetQuizQuiz Quiz { get; set; }
}

public class GetQuizQuiz
{
    public required string Id { get; set; }
    public required string Title { get; set; }
    public string Scope { get; set; }
    public required IEnumerable<GetQuizTeam> Teams { get; set; }
}

public class GetQuizTeam
{
    public required string Id { get; set; }
    public required string Title { get; set; }
    public string Scope { get; set; }
}

