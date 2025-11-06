namespace QuizBar.Api.Requests.QuizLocations;

using FluentValidation;
using FluentValidation.Results;

public class CreateQuizLocationRequest : IValidatableRequest 
{
    public required string Title { get; set; }

    public Task<ValidationResult> ValidateAsync()
    {
        return new CreateQuizLocationRequestValidator().ValidateAsync(this);
    }
}

public class CreateQuizLocationRequestValidator : AbstractValidator<CreateQuizLocationRequest>
{
    public CreateQuizLocationRequestValidator()
    {
        RuleFor(quizLocation => quizLocation.Title).NotEmpty();
    }
}