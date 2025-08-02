using Foksty.Api.Requests;
using Foksty.Data.Entities;
using Foksty.Shared;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace Foksty.Api.Controllers;

public class AuthController(
    Statuses statuses,
    Roles roles,
    UserManager<FokstyUser> userManager,
    IConfiguration configuration) : BaseController
{
    [HttpPost("signup")]
    public async Task<IActionResult> SignUp(SignUpRequest model)
    {
        var userExists = await userManager.FindByEmailAsync(model.Email);
        if (userExists is not null)
        {
            return BadRequest("User already exists");
        }

        FokstyUser user = new()
        {
            Email = model.Email,
            SecurityStamp = Guid.NewGuid().ToString(),
            UserName = model.Email,
            EmailConfirmed = true,
            Status = statuses.Active,
            CreatedBy = model.Email,
            CreatedOn = DateTime.UtcNow
        };

        var createUserResult = await userManager.CreateAsync(user, model.Password);
        if (!createUserResult.Succeeded)
        {
            return BadRequest("Failed to create user");
        }

        var addUserToRoleResult = await userManager.AddToRoleAsync(user, roles.Default);
        if (!addUserToRoleResult.Succeeded)
        {
            return BadRequest($"Failed to add user to role { roles.Default }");
        }

        return Created();
    }


    [HttpPost("signin")]
    public async Task<ActionResult<string>> SignIn(SignInRequest request)
    {
        var user = await userManager.FindByEmailAsync(request.Email);
        if (user is null)
        {
            return Unauthorized();
        }

        var passwordCheck = await userManager.CheckPasswordAsync(user, request.Password);
        if (passwordCheck is false)
        {
            return Unauthorized();
        }

        string token = CreateToken(user);

        return Ok(token);
    }

    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
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
            Subject = new ClaimsIdentity(new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id),
                new Claim(ClaimTypes.Email, user.Email)
            }),
            Expires = DateTime.UtcNow.AddMinutes(60),
            Issuer = configuration["JWT:Issuer"],
            Audience = configuration["JWT:Audience"],
            SigningCredentials = new SigningCredentials(
                new SymmetricSecurityKey(key),
                SecurityAlgorithms.HmacSha256Signature
            )
        };

        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }
}