using FluentValidation;

namespace QuizBar.Api.Requests.Quizzes;

public class CreateQuizEventRequest
{
    public string? QuizId { get; set; }
    public string? Location { get; set; }
    public IEnumerable<string> TeamIds { get; set; }
}

public class CreateQuizEventRequestValidator : AbstractValidator<CreateQuizEventRequest>
{
    public CreateQuizEventRequestValidator()
    {
        RuleFor(atq => atq.QuizId).NotEmpty();
        RuleFor(atq => atq.TeamIds).NotEmpty();
    }
}