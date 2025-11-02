namespace QuizBar.Api.Requests.QuizLocations;

using FluentValidation;
using System.Text.Json.Serialization;

public class UpdateQuizLocationRequest
{
    [JsonIgnore]
    public int QuizLocationId { get; set; }
    public required string Title { get; set; }
}

public class UpdateQuizLocationRequestValidator : AbstractValidator<UpdateQuizLocationRequest>
{
    public UpdateQuizLocationRequestValidator()
    {
        RuleFor(quizLocation => quizLocation.QuizLocationId).NotEmpty();
        RuleFor(quizLocation => quizLocation.Title).NotEmpty();
    }
}