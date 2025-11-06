namespace QuizBar.Api.Requests.Questions;

using FluentValidation;
using FluentValidation.Results;
using QuizBar.Shared;

public class UpdateQuestionRequest : IValidatableRequest
{
    public string? QuestionId { get; set; }
    public string? Title { get; set; }
    public string? Resource { get; set; }
    public QuestionType? Type { get; set; }
    public string? ChapterId { get; set; }
    public int? Priority { get; set; }
    public IEnumerable<CreateQuestionAnswer>? Answers { get; set; }

    public Task<ValidationResult> ValidateAsync()
    {
        return new UpdateQuestionRequestValidator().ValidateAsync(this);
    }
}

public class UpdateQuestionRequestValidator : AbstractValidator<UpdateQuestionRequest>
{
    public UpdateQuestionRequestValidator()
    {
        When(question => question.Type != null, () =>
        {
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
                RuleFor(question => question.Answers.Count()).Equal(2);
                RuleFor(question => question.Answers.Count(possibleAnswer => possibleAnswer.Title == "True")).Equal(1);
                RuleFor(question => question.Answers.Count(possibleAnswer => possibleAnswer.Title == "False")).Equal(1);
                RuleFor(question => question.Answers.Count(possibleAnswer => possibleAnswer.IsCorrect == true)).Equal(1);
                RuleFor(question => question.Answers.Count(possibleAnswer => possibleAnswer.IsCorrect == false)).Equal(1);
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
        });
    }
}