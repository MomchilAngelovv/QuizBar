namespace Foksty.Api.Requests.Auth;

public class SignInRequest
{
    public required string Email { get; set; }
    public required string Password { get; set; }
}
