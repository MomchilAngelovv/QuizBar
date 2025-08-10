using FluentValidation;

namespace Foksty.Api.Requests.Teams;

public class GetTeamsRequest
{
}

public class GetTeamsRequestValidator : AbstractValidator<GetTeamsRequest>
{
    public GetTeamsRequestValidator()
    {
    }
}