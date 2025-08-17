namespace Foksty.Services.Inputs.Questions;

public class CreateQuestionServiceInput : IServiceInput
{
    public string Text { get; set; }
    public string Type { get; set; }
    public int ShowOrder { get; set; }
    public string ChapterId { get; set; }
    public IEnumerable<CreateQuestionPossibleAnswerServiceInput> PossibleAnswers { get; set; }

    //IServiceInput
    public string CreatedBy { get; set; }
}