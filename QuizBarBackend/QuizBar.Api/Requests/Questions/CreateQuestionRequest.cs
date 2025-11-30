namespace QuizBar.Api.Requests.Questions;

using FluentValidation;
using FluentValidation.Results;
using QuizBar.Shared;
using System.Threading.Tasks;

public class CreateQuestionRequest : IValidatableRequest
{
    public required string Title { get; set; }
    public string Resource { get; set; }
    public required QuestionType Type { get; set; }
    public required string ChapterId { get; set; }
    public int Priority { get; set; }
    public IEnumerable<CreateQuestionAnswer> Answers { get; set; }

    public Task<ValidationResult> ValidateAsync()
    {
        return new CreateQuestionRequestValidator().ValidateAsync(this);
    }
}

public class CreateQuestionAnswer
{
    public required string Title { get; set; }
    public bool IsCorrect { get; set; }
    public int Priority { get; set; }
}

public class CreateQuestionRequestValidator : AbstractValidator<CreateQuestionRequest>
{
    public CreateQuestionRequestValidator()
    {
        RuleFor(question => question.Title).NotEmpty();
        RuleFor(question => question.Type).NotEmpty().IsInEnum();
        RuleFor(question => question.Answers).NotEmpty();

        RuleForEach(question => question.Answers).ChildRules(pa =>
        {
            pa.RuleFor(possibleAnswer => possibleAnswer.Title).NotEmpty();
        });

        When(question => question.Type == QuestionType.SingleChoice, () =>
        {
            RuleFor(question => question.Answers.Count(possibleAnswer => possibleAnswer.IsCorrect == true)).Equal(1);
        });

        When(question => question.Type == QuestionType.MultiChoice, () =>
        {
            RuleFor(question => question.Answers.Count(possibleAnswer => possibleAnswer.IsCorrect == true)).GreaterThan(1);
        });

        When(question => question.Type == QuestionType.TrueFalse, () =>
        {
			RuleFor(question => question.Answers.Count()).Equal(1);
		});

        When(question => question.Type == QuestionType.FreeText, () =>
        {
            RuleFor(question => question.Answers.Count()).Equal(1);
			RuleFor(question => question.Answers.Count(possibleAnswer => possibleAnswer.IsCorrect == true)).Equal(1);
        });

        When(question => question.Type == QuestionType.Video || question.Type == QuestionType.Sound, () =>
        {
            RuleFor(question => question.Answers.Count()).Equal(1);
            RuleFor(question => question.Answers.Count(possibleAnswer => possibleAnswer.IsCorrect == true)).Equal(1);
            RuleFor(question => question.Resource).NotEmpty();
        });
    }
}