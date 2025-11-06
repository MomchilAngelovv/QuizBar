namespace QuizBar.Shared.ServiceModels.Inputs.Questions;

using QuizBar.Shared;

public class UpdateQuestionServiceInput : IServiceInput
{
    public required string QuestionId { get; set; }
    public string? Title { get; set; }
    public string? Resource { get; set; }
    public QuestionType? Type { get; set; }
    public string? ChapterId { get; set; }
    public int? Priority { get; set; }
    public IEnumerable<UpdateQuestionAnswerServiceInput>? Answers { get; set; }

    // IServiceInput
    public required string UserId { get; set; }
}

public class UpdateQuestionAnswerServiceInput
{
    public required string Title { get; set; }
    public bool IsCorrect { get; set; }
    public int Priority { get; set; }
}