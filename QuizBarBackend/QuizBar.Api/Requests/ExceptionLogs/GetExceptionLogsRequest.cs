namespace QuizBar.Api.Requests.ExceptionLogs;

using FluentValidation;
using FluentValidation.Results;

public class GetExceptionLogsRequest : IValidatableRequest
{
    public int? Page { get; set; } = 1;

    public async Task<ValidationResult> ValidateAsync()
    {
        return await new GetExceptionLogsRequestValidator().ValidateAsync(this);
    }
}

public class GetExceptionLogsRequestValidator : AbstractValidator<GetExceptionLogsRequest>
{
    public GetExceptionLogsRequestValidator()
    {
        RuleFor(x => x.Page).NotEmpty().GreaterThan(0);
    }
}