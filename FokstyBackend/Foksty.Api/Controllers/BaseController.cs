using FluentValidation.Results;
using Foksty.Api.Responses;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Security.Claims;

namespace Foksty.Api.Controllers;

[ApiController]
[Produces("application/json")]
[Route("api/[controller]")]
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
[ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(ControllerResult))]
[ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ControllerResult))]
[ProducesResponseType(StatusCodes.Status504GatewayTimeout, Type = typeof(ControllerResult))]
public class BaseController : ControllerBase
{
    public string LoggedInUserId => this.User.Claims.First(c => c.Type == ClaimTypes.NameIdentifier).Value;
    public string LoggedInUserEmail => this.User.Claims.First(c => c.Type == ClaimTypes.Email).Value;

    protected ActionResult ValidationFailed(ValidationResult validationResult)
    {
        foreach (var error in validationResult.Errors)
        {
            ModelState.AddModelError(error.PropertyName, error.ErrorMessage);
        }

        return ValidationProblem(ModelState);
    }

    protected IActionResult ControllerResponse<TResult>(
        TResult data, 
        bool hasError = default, 
        string message = default, 
        HttpStatusCode statusCode = HttpStatusCode.OK) where TResult : class
    {
        var controllerResult = new ControllerResponse<TResult>()
        {
            HasError = hasError,
            Message = message,
            Data = data
        };

        return StatusCode((int)statusCode, controllerResult);
    }
}