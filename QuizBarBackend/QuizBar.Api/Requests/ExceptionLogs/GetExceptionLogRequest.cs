namespace QuizBar.Api.Requests.ExceptionLogs;

using FluentValidation;
using FluentValidation.Results;
using System.Text.Json.Serialization;

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