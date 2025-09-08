using FluentValidation;
using System.Text.Json.Serialization;

namespace QuizBar.Api.Requests.Quizzes;

public class UpdateQuizEventScoresRequest
{
    [JsonIgnore]
    public string? QuizEventId { get; set; }
    public required IEnumerable<UpdateQuizEventResultsTeamResult> TeamsScores { get; set; }
}

public class UpdateQuizEventResultsTeamResult
{
    public required string TeamId { get; set; }
    public required double Score { get; set; }
}

public class UpdateQuizEventResultsRequestValidator : AbstractValidator<UpdateQuizEventScoresRequest>
{
    public UpdateQuizEventResultsRequestValidator()
    {
        RuleFor(qe => qe.QuizEventId).NotEmpty();
        RuleFor(qe => qe.TeamsScores).NotEmpty();

        RuleForEach(qq => qq.TeamsScores).ChildRules(pa =>
        {
            pa.RuleFor(pa => pa.TeamId).NotEmpty();
            pa.RuleFor(pa => pa.Score).NotEmpty();
        });
    }
}