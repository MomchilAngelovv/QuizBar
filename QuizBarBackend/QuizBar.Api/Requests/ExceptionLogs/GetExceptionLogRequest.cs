using FluentValidation;

namespace QuizBar.Api.Requests.ExceptionLogs;

public class GetExceptionLogRequest
{
    public string? ExceptionLogId { get; set; }
}

public class GetExceptionLogRequestValidator : AbstractValidator<GetExceptionLogRequest>
{
    public GetExceptionLogRequestValidator()
    {
        RuleFor(atq => atq.ExceptionLogId).NotEmpty();
    }
}