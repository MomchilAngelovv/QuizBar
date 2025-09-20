using QuizBar.Shared;

namespace QuizBar.Services.Inputs.Questions;

public class CreateQuestionServiceInput : IServiceInput
{
    public required string Text { get; set; }
    public string ResourceUrl { get; set; }
    public required QuestionType Type { get; set; }
    public required string ChapterId { get; set; }
    public int Priority { get; set; }

    public IEnumerable<CreateQuestionAnswerServiceInput>? Answers { get; set; }

    // IServiceInput
    public required string UserId { get; set; }
}

public class CreateQuestionAnswerServiceInput
{
    public required string Text { get; set; }
    public bool IsCorrect { get; set; }
    public int Priority { get; set; }
}