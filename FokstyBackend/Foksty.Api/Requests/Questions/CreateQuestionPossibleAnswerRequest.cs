namespace Foksty.Api.Requests.Questions;

public class CreateQuestionPossibleAnswerRequest
{
    public string Text { get; set; }
    public bool IsCorrect { get; set; }
    public int Priority { get; set; }
}