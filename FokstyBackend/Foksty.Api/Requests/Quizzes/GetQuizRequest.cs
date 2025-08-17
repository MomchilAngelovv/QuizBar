using FluentValidation;

namespace Foksty.Api.Requests.Quizzes;

public class GetQuizRequest
{
}

public class GetQuizRequestValidator : AbstractValidator<GetQuizRequest>
{
    public GetQuizRequestValidator()
    {
    }
}