namespace QuizBar.Api.Requests.Sites;

using FluentValidation;
using FluentValidation.Results;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

public class UpdateSiteRequest : IValidatableRequest
{
    [JsonIgnore]
    public int QuizLocationId { get; set; }
    public required string Title { get; set; }

    public Task<ValidationResult> ValidateAsync()
    {
        return new UpdateQuizLocationRequestValidator().ValidateAsync(this);
    }
}

public class UpdateQuizLocationRequestValidator : AbstractValidator<UpdateSiteRequest>
{
    public UpdateQuizLocationRequestValidator()
    {
        RuleFor(quizLocation => quizLocation.QuizLocationId).NotEmpty();
        RuleFor(quizLocation => quizLocation.Title).NotEmpty();
    }
}