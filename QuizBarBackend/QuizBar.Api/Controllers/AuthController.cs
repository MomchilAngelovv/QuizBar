namespace QuizBar.Api.Controllers;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using QuizBar.Api.Requests.Auth;
using QuizBar.Api.Responses;
using QuizBar.Api.Responses.Auth;
using QuizBar.Data.Entities;
using QuizBar.Shared;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using System.Text;

public class AuthController(
    UserManager<QuizBarUser> userManager,
    IConfiguration configuration) : BaseController
{
    [AllowAnonymous]
    [HttpPost("signup")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ControllerResponse<SignUpResponse>))]
    public async Task<IActionResult> SignUp(
        [FromBody] SignUpRequest request, [FromServices] SignUpRequestValidator requestValidator)
    {
        var validationResult = await requestValidator.ValidateAsync(request);
        if (!validationResult.IsValid)
        {
            return ValidationFailed(validationResult);
        }

        var userExists = await userManager.FindByEmailAsync(request.Email);
        if (userExists is not null)
        {
            return ControllerResponse(ValidationErrors.AlreadyExists("User"), HttpStatusCode.BadRequest);
        }

        var user = new QuizBarUser
        {
            Id = Guid.CreateVersion7().ToString(),
            Email = request.Email,
            SecurityStamp = Guid.NewGuid().ToString(),
            UserName = request.Email,
            EmailConfirmed = true,
            Status = Statuses.Active,
            CreatedBy = request.Email,
            CreatedOn = DateTime.UtcNow
        };

        var createUserResult = await userManager.CreateAsync(user, request.Password);
        if (!createUserResult.Succeeded)
        {
            return ControllerResponse(ValidationErrors.FailedToCreateUser(), HttpStatusCode.BadRequest);
        }

        var addUserToRoleResult = await userManager.AddToRoleAsync(user, Roles.Default.ToString());
        if (!addUserToRoleResult.Succeeded)
        {
            return ControllerResponse(ValidationErrors.FailedToAssignRole(), HttpStatusCode.BadRequest);
        }

        return ControllerResponse(new SignUpResponse
        {
            Id = user.Id
        });
    }

    [AllowAnonymous]
    [HttpPost("signin")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ControllerResponse<SignInResponse>))]
    public async Task<IActionResult> SignIn(
        [FromBody] SignInRequest request, [FromServices] SignInRequestValidator requestValidator)
    {
        var validationResult = await requestValidator.ValidateAsync(request);
        if (!validationResult.IsValid)
        {
            return ValidationFailed(validationResult);
        }

        var user = await userManager.FindByEmailAsync(request.Email);
        if (user is null)
        {
            return ControllerResponse("Invalid credentials", HttpStatusCode.Unauthorized);
        }

        var passwordCheck = await userManager.CheckPasswordAsync(user, request.Password);
        if (passwordCheck is false)
        {
            return ControllerResponse("Invalid credentials", HttpStatusCode.Unauthorized);
        }

        string token = CreateToken(user);

        return ControllerResponse(new SignInResponse
        {
            Token = token,
        });
    }

    [HttpPost("profile")]
    public async Task<ActionResult<object>> Profile()
    {
        var userClaims = User.Claims.Select(c => new
        {
            c.Type,
            c.Value
        });

        return new
        {
            Test = "test",
            UserClaims = userClaims
        };
    }
    private string CreateToken(QuizBarUser user)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.UTF8.GetBytes(configuration["JWT:Secret"]);

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(
            [
                new Claim(ClaimTypes.NameIdentifier, user.Id),
                new Claim(ClaimTypes.Email, user.Email)
            ]),
            Expires = DateTime.UtcNow.AddMinutes(60),
            Issuer = configuration["JWT:Issuer"],
            Audience = configuration["JWT:Audience"],
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };

        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }
}