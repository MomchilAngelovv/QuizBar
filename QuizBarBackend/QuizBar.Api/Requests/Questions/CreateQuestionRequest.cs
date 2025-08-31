using FluentValidation;
using QuizBar.Shared;

namespace QuizBar.Api.Requests.Questions;

public class CreateQuestionRequest
{
    public required string Text { get; set; }
    public required QuestionType Type { get; set; }
    public int Priority { get; set; }
    public required string ChapterId { get; set; }
    public IEnumerable<CreateQuestionPossibleAnswer>? PossibleAnswers { get; set; }
}

public class CreateQuestionPossibleAnswer
{
    public required string Text { get; set; }
    public required bool IsCorrect { get; set; }
    public int Priority { get; set; }
}

public class CreateQuestionRequestValidator : AbstractValidator<CreateQuestionRequest>
{
    public CreateQuestionRequestValidator()
    {
        RuleFor(q => q.Text).NotEmpty();
        RuleFor(q => q.Type).NotEmpty().IsInEnum();
        RuleFor(q => q.ChapterId).NotEmpty();

        When(q => q.PossibleAnswers is not null && q.PossibleAnswers.Any(), () => 
        {
            RuleForEach(x => x.PossibleAnswers).ChildRules(order =>
            {
                order.RuleFor(x => x.Text).NotEmpty();
            });
        });
    }
}