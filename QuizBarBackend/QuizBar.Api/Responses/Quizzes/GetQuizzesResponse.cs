namespace QuizBar.Api.Responses.Quizzes;

using QuizBar.Shared;

public class GetQuizzesResponse : IPaginatedControllerResponse
{
    // IPaginatedControllerResponse
    public int Page { get; set; }
    public int TotalItems { get; set; }

    public IEnumerable<GetQuizzesQuiz> Quizzes { get; set; }
}

public class GetQuizzesQuiz
{
    public string Id { get; set; }
    public string Title { get; set; }
    public Statuses Status { get; set; }
}