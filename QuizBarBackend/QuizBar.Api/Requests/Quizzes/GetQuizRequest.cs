namespace QuizBar.Api.Requests.Quizzes;

using System.Text.Json.Serialization;

using FluentValidation;
using FluentValidation.Results;

public class GetQuizRequest : IValidatableRequest
{
    [JsonIgnore]
    public string? QuizId { get; set; }

    public Task<ValidationResult> ValidateAsync()
    {
        return new GetQuizRequestValidator().ValidateAsync(this);  
    }
}

public class GetQuizRequestValidator : AbstractValidator<GetQuizRequest>
{
    public GetQuizRequestValidator()
    {
        RuleFor(x => x.QuizId).NotEmpty();
    }
}