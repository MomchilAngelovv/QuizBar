using FluentValidation;

namespace QuizBar.Api.Requests.Auth;

public class SignUpRequest
{
    public required string Email { get; set; }
    public required string Password { get; set; }
}

public class SignUpRequestValidator : AbstractValidator<SignUpRequest>
{
    public SignUpRequestValidator()
    {
        RuleFor(x => x.Email).NotEmpty();
        RuleFor(x => x.Password).NotEmpty();
    }
}