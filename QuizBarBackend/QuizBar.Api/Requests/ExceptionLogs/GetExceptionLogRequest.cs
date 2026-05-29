namespace QuizBar.Api.Requests.ExceptionLogs;

using System.Text.Json.Serialization;

using FluentValidation;
using FluentValidation.Results;

public class GetExceptionLogRequest : IValidatableRequest
{
    [JsonIgnore]
    public string? ExceptionLogId { get; set; }

    public Task<ValidationResult> ValidateAsync()
    {
        return new GetExceptionLogRequestValidator().ValidateAsync(this);
    }
}

public class GetExceptionLogRequestValidator : AbstractValidator<GetExceptionLogRequest>
{
    public GetExceptionLogRequestValidator()
    {
        RuleFor(atq => atq.ExceptionLogId).NotEmpty();
    }
}