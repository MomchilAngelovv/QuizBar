using FluentValidation;
using Foksty.Shared;

namespace Foksty.Api.Requests.Questions;

public class CreateQuestionRequest
{
    public string Text { get; set; }
    public QuestionType Type { get; set; }
    public int Priority { get; set; }
    public string ChapterId { get; set; }
    public IEnumerable<CreateQuestionPossibleAnswerRequest> PossibleAnswers { get; set; }
}

public class CreateQuestionRequestValidator : AbstractValidator<CreateQuestionRequest>
{
    public CreateQuestionRequestValidator()
    {
    }
}