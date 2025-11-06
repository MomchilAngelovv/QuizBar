namespace QuizBar.Api.Requests.Auth;

using FluentValidation;
using FluentValidation.Results;

public class SignUpRequest : IValidatableRequest
{
    public string Email { get; set; }
    public string Password { get; set; }

    public Task<ValidationResult> ValidateAsync()
    {
        return new SignUpRequestValidator().ValidateAsync(this);
    }
}

public class SignUpRequestValidator : AbstractValidator<SignUpRequest>
{
    public SignUpRequestValidator()
    {
        RuleFor(x => x.Email).NotEmpty();
        RuleFor(x => x.Password).NotEmpty();
    }
}