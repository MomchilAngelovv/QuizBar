namespace QuizBar.Api.Requests.Auth;

using FluentValidation;
using FluentValidation.Results;

public class SignInRequest : IValidatableRequest
{
    public required string Email { get; set; }
    public required string Password { get; set; }

    public Task<ValidationResult> ValidateAsync()
    {
        return new SignInRequestValidator().ValidateAsync(this);
    }
}

public class SignInRequestValidator : AbstractValidator<SignInRequest>
{
    public SignInRequestValidator()
    {
        RuleFor(x => x.Email).NotEmpty();
        RuleFor(x => x.Password).NotEmpty();
    }
}