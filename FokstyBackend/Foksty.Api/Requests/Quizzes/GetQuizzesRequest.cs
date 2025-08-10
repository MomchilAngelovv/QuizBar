using FluentValidation;

namespace Foksty.Api.Requests.Quizzes;

public class GetQuizzesRequest
{
}

public class GetQuizzesRequestValidator : AbstractValidator<GetQuizzesRequest>
{
    public GetQuizzesRequestValidator()
    {
    }
}