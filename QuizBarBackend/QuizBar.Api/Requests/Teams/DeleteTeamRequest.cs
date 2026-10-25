namespace QuizBar.Api.Requests.Teams;

using FluentValidation;
using FluentValidation.Results;

public class DeleteTeamRequest : IValidatableRequest
{
    public string TeamId { get; set; }

    public Task<ValidationResult> ValidateAsync()
    {
        return new DeleteTeamRequestValidator().ValidateAsync(this);
    }
}

public class DeleteTeamRequestValidator : AbstractValidator<DeleteTeamRequest>
{
    public DeleteTeamRequestValidator()
    {
        RuleFor(r => r.TeamId).NotEmpty();
    }
}
