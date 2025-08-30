using FluentValidation;

namespace QuizBar.Api.Requests.Auth;

public class SignInRequest
{
    public required string Email { get; set; }
    public required string Password { get; set; }
}

public class SignInRequestValidator : AbstractValidator<SignInRequest>
{
    public SignInRequestValidator()
    {
        RuleFor(x => x.Email).NotEmpty();
        RuleFor(x => x.Password).NotEmpty();
    }
}