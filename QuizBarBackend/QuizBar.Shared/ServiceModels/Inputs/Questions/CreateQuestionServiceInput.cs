namespace QuizBar.Shared.ServiceModels.Inputs.Questions;

using QuizBar.Shared;

public class CreateQuestionServiceInput : IServiceInput
{
    public required string Title { get; set; }
    public string Resource { get; set; }
    public required QuestionType Type { get; set; }
    public required string ChapterId { get; set; }
    public int Priority { get; set; }

    public IEnumerable<CreateQuestionAnswerServiceInput>? Answers { get; set; }

    // IServiceInput
    public required string UserId { get; set; }
}

public class CreateQuestionAnswerServiceInput
{
    public required string Title { get; set; }
    public bool IsCorrect { get; set; }
    public int Priority { get; set; }
}