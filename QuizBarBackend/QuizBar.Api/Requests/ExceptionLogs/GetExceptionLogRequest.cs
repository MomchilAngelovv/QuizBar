using FluentValidation;
using System.Text.Json.Serialization;

namespace QuizBar.Api.Requests.ExceptionLogs;

public class GetExceptionLogRequest
{
    [JsonIgnore]
    public string? ExceptionLogId { get; set; }
}

public class GetExceptionLogRequestValidator : AbstractValidator<GetExceptionLogRequest>
{
    public GetExceptionLogRequestValidator()
    {
        RuleFor(atq => atq.ExceptionLogId).NotEmpty();
    }
}