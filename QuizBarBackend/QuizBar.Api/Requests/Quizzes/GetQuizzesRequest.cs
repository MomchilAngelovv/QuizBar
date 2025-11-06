namespace QuizBar.Api.Requests.Quizzes;

using FluentValidation;
using FluentValidation.Results;

public class GetQuizzesRequest : IValidatableRequest
{
    public int Page { get; set; }

    public Task<ValidationResult> ValidateAsync()
    {
        return new GetQuizzesRequestValidator().ValidateAsync(this);
    }
}

public class GetQuizzesRequestValidator : AbstractValidator<GetQuizzesRequest>
{
    public GetQuizzesRequestValidator()
    {
        RuleFor(quiz => quiz.Page).NotEmpty().GreaterThan(0);
    }
}