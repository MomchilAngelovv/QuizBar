namespace QuizBar.Shared.ServiceModels.Results.Quizzes;

public class GetQuizServiceResult
{
    public string Id { get; set; }
    public string Title { get; set; }
    public string Scope { get; set; }
    public IEnumerable<GetQuizzesChapter> Chapters { get; set; }
}

public class GetQuizzesChapter
{
    public string Id { get; set; }
    public string Title { get; set; }
    public Statuses Status { get; set; }
    public IEnumerable<GetQuizzesChapterQuestion> Questions { get; set; }
}

public class GetQuizzesChapterQuestion
{
    public string Id { get; set; }
    public QuestionType Type { get; set; }
    public string Title { get; set; }
    public string Resource { get; set; }
    public int Priority { get; set; }
    public Statuses Status { get; set; }
    public IEnumerable<GetQuizzesChapterQuestionAnswer> Answers { get; set; }
}

public class GetQuizzesChapterQuestionAnswer
{
    public string Id { get; set; }
    public string Title { get; set; }
    public bool IsCorrect { get; set; }
    public int Priority { get; set; }
    public Statuses Status { get; set; }
}