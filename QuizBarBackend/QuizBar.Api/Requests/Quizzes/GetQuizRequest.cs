using FluentValidation;
using System.Text.Json.Serialization;

namespace QuizBar.Api.Requests.Quizzes;

public class GetQuizRequest
{
    [JsonIgnore]
    public string? QuizId { get; set; }
}

public class GetQuizRequestValidator : AbstractValidator<GetQuizRequest>
{
    public GetQuizRequestValidator()
    {
        RuleFor(x => x.QuizId).NotEmpty();
    }
}