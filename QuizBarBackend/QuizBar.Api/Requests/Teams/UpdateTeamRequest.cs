using FluentValidation;
using System.Text.Json.Serialization;

namespace QuizBar.Api.Requests.Teams;

public class UpdateTeamRequest
{
    [JsonIgnore]
    public string? TeamId { get; set; }
    public required string Title { get; set; }
}

public class UpdateTeamRequestValidator : AbstractValidator<UpdateTeamRequest>
{
    public UpdateTeamRequestValidator()
    {
        RuleFor(t => t.TeamId).NotEmpty();
        RuleFor(t => t.Title).NotEmpty();
    }
}