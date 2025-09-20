using FluentValidation;

namespace QuizBar.Api.Requests.Teams;

public class GetTeamRequest
{
    public string TeamId { get; set; }
}

public class GetTeamRequestValidator : AbstractValidator<GetTeamRequest>
{
    public GetTeamRequestValidator()
    {
        RuleFor(r => r.TeamId).NotEmpty();
    }
}