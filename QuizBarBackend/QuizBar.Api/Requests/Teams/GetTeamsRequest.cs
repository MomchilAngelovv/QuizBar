using FluentValidation;

namespace QuizBar.Api.Requests.Teams;

public class GetTeamsRequest
{
    public int Page { get; set; }
}

public class GetTeamsRequestValidator : AbstractValidator<GetTeamsRequest>
{
    public GetTeamsRequestValidator()
    {
        RuleFor(r => r.Page).NotEmpty();
    }
}