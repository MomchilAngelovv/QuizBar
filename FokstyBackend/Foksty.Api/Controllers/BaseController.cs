using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Foksty.Api.Controllers;

[ApiController]
[Produces("application/json")]
[Route("api/[controller]")]
//[Authorize]
//[ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(ControllerResult))]
[ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ControllerResult))]
[ProducesResponseType(StatusCodes.Status504GatewayTimeout, Type = typeof(ControllerResult))]
public class BaseController : ControllerBase
{
    protected ActionResult ValidationFailed(ValidationResult validationResult)
    {
        validationResult.AddToModelState(ModelState);
        return ValidationProblem(ModelState);
    }

    protected IActionResult ControllerResult<TResult>(ServiceResult<TResult> serviceResult)
      where TResult : class
    {
        var hasError = false;
        var errorMessage = default(string);

        if (!string.IsNullOrWhiteSpace(serviceResult.ErrorMessage))
        {
            hasError = true;
            errorMessage = serviceResult.ErrorMessage;
        }

        var controllerResult = new ControllerResult<TResult>()
        {
            HasError = hasError,
            ErrorMessage = errorMessage,
            Data = serviceResult.Data,
        };

        return StatusCode((int)serviceResult.Status, controllerResult);
    }
}
