using Foksty.Api.Requests;
using Foksty.Data.Entities;
using Foksty.Shared;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Foksty.Api.Controllers;

public class AuthenticationController(
    Statuses statuses,
    Roles roles,
    UserManager<FokstyUser> userManager) : BaseController
{
    [HttpPost("signup")]
    public async Task<IActionResult> Signup(SignUpRequest model)
    {
        var userExists = await userManager.FindByEmailAsync(model.Email);
        if (userExists != null)
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

        return CreatedAtAction(nameof(Signup), null);
    }
}