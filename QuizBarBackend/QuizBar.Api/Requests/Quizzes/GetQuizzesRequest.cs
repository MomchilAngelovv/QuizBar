using FluentValidation;

namespace QuizBar.Api.Requests.Quizzes;

public class GetQuizzesRequest
{
}

public class GetQuizzesRequestValidator : AbstractValidator<GetQuizzesRequest>
{
    public GetQuizzesRequestValidator()
    {
    }
}