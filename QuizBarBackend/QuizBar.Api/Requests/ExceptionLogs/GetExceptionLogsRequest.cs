using FluentValidation;

namespace QuizBar.Api.Requests.ExceptionLogs;

public class GetExceptionLogsRequest
{
    public int? Page { get; set; } = 1;
}

public class GetExceptionLogsRequestValidator : AbstractValidator<GetExceptionLogsRequest>
{
    public GetExceptionLogsRequestValidator()
    {
        RuleFor(x => x.Page).NotEmpty().GreaterThan(0);
    }
}