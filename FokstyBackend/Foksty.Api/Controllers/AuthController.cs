using Foksty.Api.Requests.Auth;
using Foksty.Api.Responses;
using Foksty.Api.Responses.Auth;
using Foksty.Data.Entities;
using Foksty.Shared;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using System.Text;

namespace Foksty.Api.Controllers;

public class AuthController(
    Role roles,
    UserManager<FokstyUser> userManager,
    IConfiguration configuration) : BaseController
{
    [AllowAnonymous]
    [HttpPost("signup")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ControllerResponse<SignUpResponse>))]
    public async Task<IActionResult> SignUp(
        SignUpRequest request, [FromServices] SignUpRequestValidator requestValidator)
    {
        var validationResult = await requestValidator.ValidateAsync(request);
        if (!validationResult.IsValid)
        {
            return ValidationFailed(validationResult);
        }

        var userExists = await userManager.FindByEmailAsync(request.Email);
        if (userExists is not null)
        {
            return BadRequest("User already exists");
        }

        FokstyUser user = new()
        {
            Email = request.Email,
            SecurityStamp = Guid.NewGuid().ToString(),
            UserName = request.Email,
            EmailConfirmed = true,
            Status = Status.Active,
            CreatedBy = request.Email,
            CreatedOn = DateTime.UtcNow
        };

        var createUserResult = await userManager.CreateAsync(user, request.Password);
        if (!createUserResult.Succeeded)
        {
            return ControllerResponse(true, "Failed to create user", HttpStatusCode.BadRequest);
        }

        var addUserToRoleResult = await userManager.AddToRoleAsync(user, roles.Default);
        if (!addUserToRoleResult.Succeeded)
        {
            return ControllerResponse(true, $"Failed to add user to role {roles.Default}", HttpStatusCode.BadRequest);
        }

        return ControllerResponse(new SignUpResponse
        {

        });
    }

    [AllowAnonymous]
    [HttpPost("signin")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ControllerResponse<SignInResponse>))]
    public async Task<IActionResult> SignIn(SignInRequest request, [FromServices] SignInRequestValidator requestValidator)
    {
        var validationResult = await requestValidator.ValidateAsync(request);
        if (!validationResult.IsValid)
        {
            return ValidationFailed(validationResult);
        }

        var user = await userManager.FindByEmailAsync(request.Email);
        if (user is null)
        {
            return ControllerResponse(true, "Invalid credentials", HttpStatusCode.Unauthorized);
        }

        var passwordCheck = await userManager.CheckPasswordAsync(user, request.Password);
        if (passwordCheck is false)
        {
            return ControllerResponse(true, "Invalid credentials", HttpStatusCode.Unauthorized);
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
            Type = c.Type,
            Value = c.Value
        });

        return new
        {
            Test = "test",
            UserClaims = userClaims
        };
    }

    private string CreateToken(FokstyUser user)
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