namespace QuizBar.Services.ServiceModels.Inputs.Questions;

using QuizBar.Services.ServiceModels.Inputs;
using QuizBar.Shared;

public class CreateQuestionServiceInput : IServiceInput
{
    // IServiceInput
    public required string UserId { get; set; }

    public string Title { get; set; }
    public string Resource { get; set; }
    public QuestionType Type { get; set; }
    public string ChapterId { get; set; }
    public int Priority { get; set; }

    public IEnumerable<CreateQuestionAnswerServiceInput> Answers { get; set; }
}

public class CreateQuestionAnswerServiceInput
{
    public string Title { get; set; }
    public bool IsCorrect { get; set; }
    public int Priority { get; set; }
}