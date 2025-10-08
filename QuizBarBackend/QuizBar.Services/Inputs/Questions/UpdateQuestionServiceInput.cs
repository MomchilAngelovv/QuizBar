using QuizBar.Shared;

namespace QuizBar.Services.Inputs.Questions;

public class UpdateQuestionServiceInput : IServiceInput
{
    public required string QuestionId { get; set; }
    public string? Text { get; set; }
    public string? ResourceUrl { get; set; }
    public QuestionType? Type { get; set; }
    public string? ChapterId { get; set; }
    public int? Priority { get; set; }
    public IEnumerable<UpdateQuestionAnswerServiceInput>? Answers { get; set; }

    // IServiceInput
    public required string UserId { get; set; }
}

public class UpdateQuestionAnswerServiceInput
{
    public required string Text { get; set; }
    public bool IsCorrect { get; set; }
    public int Priority { get; set; }
}