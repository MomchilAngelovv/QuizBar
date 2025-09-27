using FluentValidation;

namespace QuizBar.Api.Requests.Quizzes;

public class GetQuizzesRequest
{
    public int Page { get; set; }
}

public class GetQuizzesRequestValidator : AbstractValidator<GetQuizzesRequest>
{
    public GetQuizzesRequestValidator()
    {
        RuleFor(quiz => quiz.Page).NotEmpty().GreaterThan(0);
    }
}