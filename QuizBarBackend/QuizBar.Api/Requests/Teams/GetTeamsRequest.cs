using FluentValidation;

namespace QuizBar.Api.Requests.Teams;

public class GetTeamsRequest
{
}

public class GetTeamsRequestValidator : AbstractValidator<GetTeamsRequest>
{
    public GetTeamsRequestValidator()
    {
    }
}