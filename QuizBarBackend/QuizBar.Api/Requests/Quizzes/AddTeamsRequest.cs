using FluentValidation;

namespace QuizBar.Api.Requests.Quizzes;

public class AddTeamsRequest
{
    public string? QuizId { get; set; }
    public IEnumerable<string> TeamIds { get; set; }
}

public class AddTeamsRequestValidator : AbstractValidator<AddTeamsRequest>
{
    public AddTeamsRequestValidator()
    {
    }
}