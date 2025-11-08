namespace QuizBar.Api.Requests.Sites;

using FluentValidation;
using FluentValidation.Results;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

public class UpdateSiteRequest : IValidatableRequest
{
    [JsonIgnore]
    public int SiteId { get; set; }
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
        RuleFor(quizLocation => quizLocation.SiteId).NotEmpty();
        RuleFor(quizLocation => quizLocation.Title).NotEmpty();
    }
}