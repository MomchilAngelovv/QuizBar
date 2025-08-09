using FluentValidation;

namespace Foksty.Api.Requests.Auth;

public class SignUpRequest
{
    public string Email { get; set; }
    public string Password { get; set; }
}

public class SignUpRequestValidator : AbstractValidator<SignUpRequest>
{
    public SignUpRequestValidator()
    {
        RuleFor(x => x.Email).NotEmpty();
        RuleFor(x => x.Password).NotEmpty();
    }
}