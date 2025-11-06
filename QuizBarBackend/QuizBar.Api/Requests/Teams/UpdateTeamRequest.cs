namespace QuizBar.Api.Requests.Teams;

using FluentValidation;
using FluentValidation.Results;
using System.Text.Json.Serialization;

public class UpdateTeamRequest : IValidatableRequest
{
    [JsonIgnore]
    public string? TeamId { get; set; }
    public required string Title { get; set; }

    public Task<ValidationResult> ValidateAsync()
    {
        return new UpdateTeamRequestValidator().ValidateAsync(this);
    }
}

public class UpdateTeamRequestValidator : AbstractValidator<UpdateTeamRequest>
{
    public UpdateTeamRequestValidator()
    {
        RuleFor(t => t.TeamId).NotEmpty();
        RuleFor(t => t.Title).NotEmpty();
    }
}