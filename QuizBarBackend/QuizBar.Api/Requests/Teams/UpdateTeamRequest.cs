using FluentValidation;
using System.Text.Json.Serialization;

namespace QuizBar.Api.Requests.Teams;

public class UpdateTeamRequest
{
    [JsonIgnore]
    public string? Id { get; set; }
    public string? Title { get; set; }
}

public class UpdateTeamRequestValidator : AbstractValidator<UpdateTeamRequest>
{
    public UpdateTeamRequestValidator()
    {
        RuleFor(t => t.Id).NotEmpty();
    }
}