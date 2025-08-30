using FluentValidation;

namespace QuizBar.Api.Requests.Quizzes;

public class CreateQuizRequest
{
    public required string Title { get; set; }
    public string? Scope { get; set; }
}

public class CreateQuizRequestValidator : AbstractValidator<CreateQuizRequest>
{
    public CreateQuizRequestValidator()
    {
        RuleFor(x => x.Title).NotEmpty();
    }
}