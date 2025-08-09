using FluentValidation;

namespace Foksty.Api.Requests.Quizzes;

public class CreateQuizRequest
{
    public string Title { get; set; }
    public string? Scope { get; set; }
}

public class CreateQuizRequestValidator : AbstractValidator<CreateQuizRequest>
{
    public CreateQuizRequestValidator()
    {
        RuleFor(x => x.Title).NotEmpty();
    }
}