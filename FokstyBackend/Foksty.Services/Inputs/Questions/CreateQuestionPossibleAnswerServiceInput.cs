namespace Foksty.Services.Inputs.Questions;

public class CreateQuestionPossibleAnswerServiceInput
{
    public string Text { get; set; }
    public bool IsCorrect { get; set; }
    public int ShowOrder { get; set; }
}
