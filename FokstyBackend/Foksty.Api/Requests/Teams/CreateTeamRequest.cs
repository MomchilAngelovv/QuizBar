using FluentValidation;

namespace Foksty.Api.Requests.Teams;

public class CreateTeamRequest
{
    public string Title { get; set; } 
}

public class CreateTeamRequestValidator : AbstractValidator<CreateTeamRequest>
{
    public CreateTeamRequestValidator()
    {
        RuleFor(x => x.Title).NotEmpty();
    }
}