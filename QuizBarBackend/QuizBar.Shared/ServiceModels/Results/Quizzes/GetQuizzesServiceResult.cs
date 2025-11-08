namespace QuizBar.Shared.ServiceModels.Results.Quizzes;

public class GetQuizzesServiceResult : IPaginatedServiceResult
{
    // IPaginatedServiceResult
    public int Page { get; set; }
    public int TotalItems { get; set; }

    public required IEnumerable<GetQuizzesQuiz> Quizzes { get; set; }
}

public class GetQuizzesQuiz
{
    public string Id { get; set; }
    public string Title { get; set; }
    public string Scope { get; set; }
    public Statuses Status { get; set; }
}