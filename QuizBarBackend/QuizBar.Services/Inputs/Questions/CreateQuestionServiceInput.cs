using QuizBar.Services.Inputs;
using QuizBar.Shared;

namespace QuizBar.Services.Inputs.Questions;

public class CreateQuestionServiceInput : IServiceInput
{
    public required string Text { get; set; }
    public required QuestionType Type { get; set; }
    public int Priority { get; set; }
    public required string ChapterId { get; set; }
    public IEnumerable<CreateQuestionPossibleAnswerServiceInput> PossibleAnswers { get; set; }

    //IServiceInput
    public required string CreatedBy { get; set; }
}

public class CreateQuestionPossibleAnswerServiceInput
{
    public required string Text { get; set; }
    public required bool IsCorrect { get; set; }
    public int Priority { get; set; }
}