using FluentValidation;
using System.Text.Json.Serialization;

namespace QuizBar.Api.Requests.QuizLocations;

public class UpdateQuizLocationRequest
{
    [JsonIgnore]
    public int QuizLocationId { get; set; }
    public required string Site { get; set; }
}

public class UpdateQuizLocationRequestValidator : AbstractValidator<UpdateQuizLocationRequest>
{
    public UpdateQuizLocationRequestValidator()
    {
        RuleFor(quizLocation => quizLocation.QuizLocationId).NotEmpty();
        RuleFor(quizLocation => quizLocation.Site).NotEmpty();
    }
}