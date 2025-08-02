using FluentValidation.Results;
using Foksty.Api.Responses;
using Foksty.Services.Results;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

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
    protected ActionResult ValidationFailed(ValidationResult validationResult)
    {
        // TODO: Check this seems like deprecated
        // validationResult.AddToModelState(ModelState);
        return ValidationProblem(ModelState);
    }

    protected IActionResult ControllerResult<TResult>(ServiceResult<TResult> serviceResult)
      where TResult : class
    {
        var hasError = false;
        var errorMessage = default(string);

        if (!string.IsNullOrWhiteSpace(serviceResult.Message))
        {
            hasError = true;
            errorMessage = serviceResult.Message;
        }

        var controllerResult = new ControllerResult<TResult>()
        {
            HasError = hasError,
            Message = errorMessage,
            Data = serviceResult.Data,
        };

        return StatusCode((int)serviceResult.Status, controllerResult);
    }
}
