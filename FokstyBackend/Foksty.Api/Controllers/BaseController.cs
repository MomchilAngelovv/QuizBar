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
[ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(ControllerResponse))]
[ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ControllerResponse))]
[ProducesResponseType(StatusCodes.Status504GatewayTimeout, Type = typeof(ControllerResponse))]
public class BaseController : ControllerBase
{
    public string UserId => this.User.Claims.First(c => c.Type == ClaimTypes.NameIdentifier).Value;
    public string UserEmail => this.User.Claims.First(c => c.Type == ClaimTypes.Email).Value;

    protected ActionResult ValidationFailed(ValidationResult validationResult)
    {
        foreach (var error in validationResult.Errors)
        {
            ModelState.AddModelError(error.PropertyName, error.ErrorMessage);
        }

        return ValidationProblem(ModelState);
    }

    protected IActionResult ControllerResponse(
        bool success = false,
        string message = default,
        HttpStatusCode statusCode = HttpStatusCode.OK)
    {
        var controllerResult = new ControllerResponse
        {
            Success = success,
            Message = message,
        };

        return StatusCode((int)statusCode, controllerResult);
    }

    protected IActionResult ControllerResponse<TResult>(
        TResult data, 
        bool success = true, 
        string message = default,
        HttpStatusCode statusCode = HttpStatusCode.OK) where TResult : class
    {
        var controllerResult = new ControllerResponse<TResult>()
        {
            Success = success,
            Message = message,
            Data = data
        };

        return StatusCode((int)statusCode, controllerResult);
    }
}