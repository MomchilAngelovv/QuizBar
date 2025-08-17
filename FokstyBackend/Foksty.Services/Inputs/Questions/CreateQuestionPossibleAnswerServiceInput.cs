namespace Foksty.Services.Inputs.Questions;

public class CreateQuestionPossibleAnswerServiceInput
{
    public required string Text { get; set; }
    public required bool IsCorrect { get; set; }
    public int Priority { get; set; }
}