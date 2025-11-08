namespace QuizBar.Api.Controllers;

using FluentValidation.Results;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using QuizBar.Api.Responses;
using QuizBar.Shared;
using QuizBar.Shared.ServiceModels.Results;
using System.Net;
using System.Security.Claims;

[ApiController]
[Produces("application/json")]
[Route("api/[controller]")]
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
[ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(ControllerResponse))]
[ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ControllerResponse))]
[ProducesResponseType(StatusCodes.Status504GatewayTimeout, Type = typeof(ControllerResponse))]
public class BaseController : ControllerBase
{
    public string UserId => User.Claims.First(c => c.Type == ClaimTypes.NameIdentifier).Value;
    public string UserEmail => User.Claims.First(c => c.Type == ClaimTypes.Email).Value;

    protected ActionResult ValidationFailed(ValidationResult validationResult)
    {
        foreach (var error in validationResult.Errors)
        {
            ModelState.AddModelError(error.PropertyName, error.ErrorMessage);
        }

        return ValidationProblem(ModelState);
    }
    protected IActionResult ControllerResponse(string message = default, HttpStatusCode statusCode = HttpStatusCode.OK)
    {
        var controllerResult = new ControllerResponse
        {
            Success = false,    
            Message = message,
        };

        return StatusCode((int)statusCode, controllerResult);
    }
    protected IActionResult ControllerResponse<TResult>(TResult data = null, bool success = true, string message = default, HttpStatusCode statusCode = HttpStatusCode.OK) where TResult : class
    {
        var controllerResult = new ControllerResponse<TResult>()
        {
            Success = success,
            Message = message,
            Data = data
        };

        return StatusCode((int)statusCode, controllerResult);
    }
    protected IActionResult ControllerResponse(ErrorReason errorReason)
    {
        var message = string.Empty;
        var statusCode = default(HttpStatusCode);

        switch (errorReason)
        {
            case ErrorReason.None:
                break;
            case ErrorReason.NotFound:
                break;
            case ErrorReason.QuestionNotFound:
                message = ValidationErrors.NotFound("Question");
                statusCode = HttpStatusCode.NotFound;
                break;
            case ErrorReason.ChapterNotFound:
                message = ValidationErrors.NotFound("Chapter");
                statusCode = HttpStatusCode.NotFound;
                break;
            case ErrorReason.QuizNotFound:
                message = ValidationErrors.NotFound("Quiz");
                statusCode = HttpStatusCode.NotFound;
                break;
            case ErrorReason.TeamNotFound:
                message = ValidationErrors.NotFound("Team");
                statusCode = HttpStatusCode.NotFound;
                break;
            case ErrorReason.AlreadyExists:
                break;
            case ErrorReason.QuestionAlreadyExists:
                message = ValidationErrors.AlreadyExists("Question");
                statusCode = HttpStatusCode.Conflict;
                break;
            case ErrorReason.ChapterAlreadyExists:
                message = ValidationErrors.AlreadyExists("Chapter");
                statusCode = HttpStatusCode.Conflict;
                break;
            case ErrorReason.QuizAlreadyExists:
                message = ValidationErrors.AlreadyExists("Quiz");
                statusCode = HttpStatusCode.Conflict;
                break;
            case ErrorReason.TeamAlreadyExists:
                message = ValidationErrors.AlreadyExists("Team");
                statusCode = HttpStatusCode.Conflict;
                break;
            case ErrorReason.IncompatibleData:
                message = ValidationErrors.AlreadyExists("INCOMBATIBLE DATA NEED TO FINISH THIS PART");
                break;
            default:
                message = "Unknown error - need to add this in the method for errros";
                break;
        }

        var controllerResult = new ControllerResponse
        {
            Success = false,
            Message = message,
        };

        return StatusCode((int)statusCode, controllerResult);
    }
}