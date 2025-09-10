using QuizBar.Shared;

namespace QuizBar.Api.Responses.Quizzes;

public class GetQuizzesResponse : IPaginationResponse
{
    public required IEnumerable<GetQuizzesQuiz> Quizzes { get; set; }
    public required int Page { get; set; }
    public required int TotalItems { get; set; }
}

public class GetQuizzesQuiz
{
    public required string Id { get; set; }
    public required string Title { get; set; }
    public required Statuses Status { get; set; }
}