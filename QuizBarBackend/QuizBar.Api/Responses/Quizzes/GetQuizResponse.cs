namespace QuizBar.Api.Responses.Quizzes;

using QuizBar.Shared;

public class GetQuizResponse
{
    public required string Id { get; set; }
    public required string Title { get; set; }
    public string Scope { get; set; }

    public required IEnumerable<GetQuizzesChapter> Chapters { get; set; }
}

public class GetQuizzesChapter
{
    public required string Id { get; set; }
    public required string Title { get; set; }
    public required Statuses Status { get; set; }

    public required IEnumerable<GetQuizzesChapterQuestion> Questions { get; set; }
}

public class GetQuizzesChapterQuestion
{
    public required string Id { get; set; }
    public required QuestionType Type { get; set; }
    public required string Title { get; set; }
    public string Resource { get; set; }
    public int Priority { get; set; }
    public required Statuses Status { get; set; }

    public required IEnumerable<GetQuizzesChapterQuestionAnswer> Answers { get; set; }
}

public class GetQuizzesChapterQuestionAnswer
{
    public required string Id { get; set; }
    public required string Title { get; set; }
    public required bool IsCorrect { get; set; }
    public int Priority { get; set; }

    public required Statuses Status { get; set; }
}