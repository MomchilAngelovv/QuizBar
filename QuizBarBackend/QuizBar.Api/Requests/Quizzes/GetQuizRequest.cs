using FluentValidation;

namespace QuizBar.Api.Requests.Quizzes;

public class GetQuizRequest
{
    public string Id { get; set; }
}

public class GetQuizRequestValidator : AbstractValidator<GetQuizRequest>
{
    public GetQuizRequestValidator()
    {
        RuleFor(x => x.Id).NotEmpty();
    }
}