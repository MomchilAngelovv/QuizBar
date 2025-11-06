using FluentValidation;
using FluentValidation.Results;

namespace QuizBar.Api.Requests.Teams;

public class CreateTeamRequest : IValidatableRequest
{
    public required string Title { get; set; }

    public Task<ValidationResult> ValidateAsync()
    {
        return new CreateTeamRequestValidator().ValidateAsync(this);
    }
}

public class CreateTeamRequestValidator : AbstractValidator<CreateTeamRequest>
{
    public CreateTeamRequestValidator()
    {
        RuleFor(x => x.Title).NotEmpty();
    }
}