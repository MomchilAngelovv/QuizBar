namespace QuizBar.Api.Requests.Sites;

using FluentValidation;
using FluentValidation.Results;

public class CreateSiteRequest : IValidatableRequest 
{
    public required string Title { get; set; }

    public Task<ValidationResult> ValidateAsync()
    {
        return new CreateQuizLocationRequestValidator().ValidateAsync(this);
    }
}

public class CreateQuizLocationRequestValidator : AbstractValidator<CreateSiteRequest>
{
    public CreateQuizLocationRequestValidator()
    {
        RuleFor(quizLocation => quizLocation.Title).NotEmpty();
    }
}