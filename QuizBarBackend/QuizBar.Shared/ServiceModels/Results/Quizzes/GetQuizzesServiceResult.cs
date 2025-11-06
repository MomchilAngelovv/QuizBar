using QuizBar.Shared;

namespace QuizBar.Shared.ServiceModels.Results.Quizzes;

public class GetQuizzesServiceResult : IPaginatedServiceResult
{
    public required IEnumerable<GetQuizzesQuiz> Quizzes { get; set; }

    // IPaginatedServiceResult
    public required int Page { get; set; }
    public required int TotalItems { get; set; }
}

public class GetQuizzesQuiz
{
    public required string Id { get; set; }
    public required string Title { get; set; }
    public string Scope { get; set; }
    public required Statuses Status { get; set; }
}