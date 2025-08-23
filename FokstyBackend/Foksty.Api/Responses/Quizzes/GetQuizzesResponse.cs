using Foksty.Shared;

namespace Foksty.Api.Responses.Quizzes;

public class GetQuizzesResponse : IPaginationResponse
{
    public IEnumerable<GetQuizzesQuiz> Quizzes { get; set; }
    public int Page { get; set; }
    public int TotalItems { get; set; }
}

public class GetQuizzesQuiz
{
    public required string Id { get; set; }
    public required string Title { get; set; }
    public Status Status { get; set; }
}