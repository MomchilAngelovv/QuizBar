namespace QuizBar.Api.Requests.QuizLocations;

using FluentValidation;
using FluentValidation.Results;

public class GetQuizLocationsRequest : IValidatableRequest
{
    public int Page { get; set; }

    public Task<ValidationResult> ValidateAsync()
    {
        return new GetQuizLocationsRequestValidator().ValidateAsync(this);
    }
}

public class GetQuizLocationsRequestValidator : AbstractValidator<GetQuizLocationsRequest>
{
    public GetQuizLocationsRequestValidator()
    {
        RuleFor(quizLocation => quizLocation.Page).NotEmpty();
    }
}