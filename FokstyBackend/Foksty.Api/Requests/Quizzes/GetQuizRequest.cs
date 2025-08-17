using FluentValidation;

namespace Foksty.Api.Requests.Quizzes;

public class GetQuizRequest
{
    public string Id { get; set; }
}

public class GetQuizRequestValidator : AbstractValidator<GetQuizRequest>
{
    public GetQuizRequestValidator()
    {
    }
}