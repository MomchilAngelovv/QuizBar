using FluentValidation;

namespace QuizBar.Api.Requests.QuizLocations; 

public class CreateQuizLocationRequest
{
    public required string Title { get; set; }
}

public class CreateQuizLocationRequestValidator : AbstractValidator<CreateQuizLocationRequest>
{
    public CreateQuizLocationRequestValidator()
    {
        RuleFor(quizLocation => quizLocation.Title).NotEmpty();
    }
}