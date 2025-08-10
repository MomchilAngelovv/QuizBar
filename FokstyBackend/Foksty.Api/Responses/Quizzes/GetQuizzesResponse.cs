using Foksty.Data.Entities;

namespace Foksty.Api.Responses.Quizzes;

public class GetQuizzesResponse : IPaginationResponse
{
    public IEnumerable<Quiz> Quizzes { get; set; }
    public int Page { get; set; }
    public int TotalItems { get; set; }
}