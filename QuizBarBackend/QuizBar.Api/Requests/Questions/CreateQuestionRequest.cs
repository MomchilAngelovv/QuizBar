using FluentValidation;
using QuizBar.Shared;

namespace QuizBar.Api.Requests.Questions;

public class CreateQuestionRequest
{
    public required string Text { get; set; }
    public required QuestionType Type { get; set; }
    public int Priority { get; set; }
    public required string ChapterId { get; set; }
    public bool? IsTrue { get; set; }
    public IEnumerable<CreateQuestionPossibleAnswer>? PossibleAnswers { get; set; }
}

public class CreateQuestionPossibleAnswer
{
    public required string Text { get; set; }
    public bool? IsCorrect { get; set; }
    public int Priority { get; set; }
}

public class CreateQuestionRequestValidator : AbstractValidator<CreateQuestionRequest>
{
    public CreateQuestionRequestValidator()
    {
        RuleFor(q => q.Text).NotEmpty();
        RuleFor(q => q.Type).NotEmpty().IsInEnum();

        When(q => q.Type == QuestionType.SingleChoice || q.Type == QuestionType.MultiChoice, () =>
        {
            RuleFor(q => q.IsTrue).Empty();
            RuleFor(q => q.PossibleAnswers).NotEmpty();

            When(q => q.Type == QuestionType.SingleChoice, () =>
            {
                RuleFor(q => q.PossibleAnswers.Count(pa => pa.IsCorrect == true)).Equal(1);
            });

            When(q => q.Type == QuestionType.MultiChoice, () =>
            {
                RuleFor(q => q.PossibleAnswers.Count(pa => pa.IsCorrect == true)).GreaterThan(1);
            });

            RuleForEach(q => q.PossibleAnswers).ChildRules(pa =>
            {
                pa.RuleFor(pa => pa.Text).NotEmpty();
            });
        });

        When(q => q.Type == QuestionType.TrueFalse, () =>
        {
            RuleFor(q => q.IsTrue).NotEmpty();
            RuleFor(q => q.PossibleAnswers).Empty();
        });

        When(q => q.Type == QuestionType.FreeText, () =>
        {
            RuleFor(q => q.IsTrue).Empty();
            RuleFor(q => q.PossibleAnswers).NotEmpty();

            RuleFor(q => q.PossibleAnswers.Count()).Equal(1);

            RuleForEach(q => q.PossibleAnswers).ChildRules(pa =>
            {
                pa.RuleFor(pa => pa.Text).NotEmpty();
                pa.RuleFor(pa => pa.IsCorrect).Empty();
            });
        });
    }
}