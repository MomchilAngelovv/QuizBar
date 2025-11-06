namespace QuizBar.Api.Requests.Teams;

using FluentValidation;
using FluentValidation.Results;

public class GetTeamRequest : IValidatableRequest
{
    public string TeamId { get; set; }

    public Task<ValidationResult> ValidateAsync()
    {
        return new GetTeamRequestValidator().ValidateAsync(this);
    }
}

public class GetTeamRequestValidator : AbstractValidator<GetTeamRequest>
{
    public GetTeamRequestValidator()
    {
        RuleFor(r => r.TeamId).NotEmpty();
    }
}