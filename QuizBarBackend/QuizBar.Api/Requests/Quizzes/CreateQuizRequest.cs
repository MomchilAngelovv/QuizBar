namespace QuizBar.Api.Requests.Quizzes;

using FluentValidation;
using FluentValidation.Results;

public class CreateQuizRequest : IValidatableRequest
{
    public required string Title { get; set; }
    public string? Scope { get; set; }

    public Task<ValidationResult> ValidateAsync()
    {
        return new CreateQuizRequestValidator().ValidateAsync(this);
    }
}

public class CreateQuizRequestValidator : AbstractValidator<CreateQuizRequest>
{
    public CreateQuizRequestValidator()
    {
        RuleFor(x => x.Title).NotEmpty();
    }
}