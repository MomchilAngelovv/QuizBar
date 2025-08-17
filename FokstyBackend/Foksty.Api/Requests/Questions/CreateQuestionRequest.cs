using FluentValidation;

namespace Foksty.Api.Requests.Questions;

public class CreateQuestionRequest
{
    public string Text { get; set; }
    public string Type { get; set; }
    public int ShowOrder { get; set; }
    public string ChapterId { get; set; }
    public IEnumerable<CreateQuestionPossibleAnswerRequest> PossibleAnswers { get; set; }
}

public class CreateQuestionRequestValidator : AbstractValidator<CreateQuestionRequest>
{
    public CreateQuestionRequestValidator()
    {
    }
}