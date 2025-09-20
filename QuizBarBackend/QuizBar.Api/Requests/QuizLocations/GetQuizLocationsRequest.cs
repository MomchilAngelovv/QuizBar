using FluentValidation;

namespace QuizBar.Api.Requests.QuizLocations;

public class GetQuizLocationsRequest
{
    public int Page { get; set; }
}

public class GetQuizLocationsRequestValidator : AbstractValidator<GetQuizLocationsRequest>
{
    public GetQuizLocationsRequestValidator()
    {
        RuleFor(quizLocation => quizLocation.Page).NotEmpty();
    }
}