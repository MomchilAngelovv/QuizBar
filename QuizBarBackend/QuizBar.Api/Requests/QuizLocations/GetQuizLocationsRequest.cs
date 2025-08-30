using FluentValidation;

namespace QuizBar.Api.Requests.QuizLocations;

public class GetQuizLocationsRequest
{

}

public class GetQuizLocationsRequestValidator : AbstractValidator<GetQuizLocationsRequest>
{
    public GetQuizLocationsRequestValidator()
    {
    }
}