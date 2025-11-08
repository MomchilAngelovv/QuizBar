namespace QuizBar.Api.Requests.Sites;

using FluentValidation;
using FluentValidation.Results;

public class GetSitesRequest : IValidatableRequest
{
    public int Page { get; set; }

    public Task<ValidationResult> ValidateAsync()
    {
        return new GetQuizLocationsRequestValidator().ValidateAsync(this);
    }
}

public class GetQuizLocationsRequestValidator : AbstractValidator<GetSitesRequest>
{
    public GetQuizLocationsRequestValidator()
    {
        RuleFor(quizLocation => quizLocation.Page).NotEmpty();
    }
}