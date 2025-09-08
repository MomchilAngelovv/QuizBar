using FluentValidation;
using System.Text.Json.Serialization;

namespace QuizBar.Api.Requests.Quizzes;

public class CreateQuizEventRequest
{
    [JsonIgnore]
    public string? QuizId { get; set; }
    public required DateTime ScheduledOn { get; set; }
    public required string QuizLocationId { get; set; }
    public IEnumerable<string>? TeamIds { get; set; }
}

public class CreateQuizEventRequestValidator : AbstractValidator<CreateQuizEventRequest>
{
    public CreateQuizEventRequestValidator()
    {
        RuleFor(qe => qe.QuizId).NotEmpty();
        RuleFor(qe => qe.ScheduledOn).NotEmpty();
        RuleFor(qe => qe.QuizLocationId).NotEmpty();

        RuleForEach(q => q.TeamIds).ChildRules(tid =>
        {
            tid.RuleFor(pa => pa).NotEmpty();
        });
    }
}