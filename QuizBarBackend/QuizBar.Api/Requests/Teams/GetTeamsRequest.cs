namespace QuizBar.Api.Requests.Teams;

using FluentValidation;
using FluentValidation.Results;

public class GetTeamsRequest : IValidatableRequest
{
    public int Page { get; set; }

    public Task<ValidationResult> ValidateAsync()
    {
        return new GetTeamsRequestValidator().ValidateAsync(this);
    }
}

public class GetTeamsRequestValidator : AbstractValidator<GetTeamsRequest>
{
    public GetTeamsRequestValidator()
    {
        RuleFor(quiz => quiz.Page).NotEmpty().GreaterThan(0);
    }
}