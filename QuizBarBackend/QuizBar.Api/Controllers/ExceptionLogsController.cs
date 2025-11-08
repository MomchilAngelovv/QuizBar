namespace QuizBar.Api.Controllers;

using Microsoft.AspNetCore.Mvc;
using QuizBar.Api.Requests.ExceptionLogs;
using QuizBar.Api.Responses;
using QuizBar.Api.Responses.ExceptionLogs;
using QuizBar.Services;
using QuizBar.Shared;
using QuizBar.Shared.ServiceModels.Inputs.ExceptionLogs;
using QuizBar.Shared.ServiceModels.Results;
using System.Net;

public class ExceptionLogsController(
    ExceptionLogsService exceptionLogsService) : BaseController
{
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ControllerResponse<GetExceptionLogsResponse>))]
    public async Task<IActionResult> GetExceptionLogsAsync(int page = 1)
    {
        var request = new GetExceptionLogsRequest { Page = page };

        var validationResult = await request.ValidateAsync();
        if (!validationResult.IsValid)
        {
            return ValidationFailed(validationResult);
        }

        var serviceInput = new GetExceptionLogsServiceInput 
        { 
            Page = request.Page.Value
        };

        var servieResult = await exceptionLogsService.GetExceptionLogsAsync(serviceInput);

        return ControllerResponse(new GetExceptionLogsResponse
        {
             Exceptions = servieResult.Result.Exceptions.Select(exception => new GetExceptionLogsException 
             { 
                 Id = exception.Id,
                 Message = exception.Message,
                 Path = exception.Path,
                 QueryString = exception.QueryString,
                 RouteValues = exception.RouteValues,
                 Headers = exception.Headers,
                 OccurredOn = exception.OccurredOn,
             }),
             Page = servieResult.Result.Page,
             TotalItems = servieResult.Result.TotalItems
        });
    }

    [HttpGet("{exceptionLogId:required}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ControllerResponse<GetExceptionLogResponse>))]
    public async Task<IActionResult> GetExceptionLogAsync(string exceptionLogId = default)
    {
        var request = new GetExceptionLogRequest { ExceptionLogId = exceptionLogId };

        var validationResult = await request.ValidateAsync();
        if (!validationResult.IsValid)
        {
            return ValidationFailed(validationResult);
        }
        
        var serviceInput = new GetExceptionLogServiceInput 
        { 
            ExceptionLogId = request.ExceptionLogId 
        };

        var serviceResult = await exceptionLogsService.GetExceptionLogAsync(serviceInput);
        if (serviceResult.Reason != ErrorReason.None)
        {
            if (serviceResult.Reason == ErrorReason.NotFound)
            {
                return ControllerResponse(ValidationErrors.NotFound("Quiz"), HttpStatusCode.NotFound);
            }
        }

        return ControllerResponse(new GetExceptionLogResponse
        {
            Id = serviceResult.Result.Id,
            Message = serviceResult.Result.Message,
            Path = serviceResult.Result.Path,
            QueryString = serviceResult.Result.QueryString,
            RouteValues = serviceResult.Result.RouteValues,
            Headers = serviceResult.Result.Headers,
            OccurredOn = serviceResult.Result.OccurredOn,
        });
    }
}