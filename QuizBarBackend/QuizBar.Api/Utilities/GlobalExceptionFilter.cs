namespace QuizBar.Api.Utilities;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using QuizBar.Services;
using QuizBar.Services.ServiceModels.Inputs.ExceptionLogs;

public class GlobalExceptionFilter(ExceptionLogsService exceptionLogsService) : IAsyncExceptionFilter
{
    //TODO: Make a service in the middle ware to save request body and pass it here if error occurs
    public async Task OnExceptionAsync(ExceptionContext context)
    {
        var request = context.HttpContext.Request;

        string queryString = request.QueryString.HasValue ? request.QueryString.Value : "";
        string routeValues = string.Join(", ", request.RouteValues.Select(r => $"{r.Key}={r.Value}"));
        string headers = string.Join("; ", request.Headers.Select(h => $"{h.Key}: {h.Value}"));

        var serviceInput = new CreateExceptionLogServiceInput
        {
            Message = context.Exception.Message,
            StackTrace = context.Exception.StackTrace,
            Path = request.Path,
            QueryString = queryString,
            RouteValues = routeValues,
            Headers = headers,
        };

        var serviceResult = await exceptionLogsService.CreateExceptionLogAsync(serviceInput);

        context.Result = new ObjectResult($"An unexpected error occurred. ExceptionLog Id: {serviceResult.Result.Id}") { StatusCode = 500 };
        context.ExceptionHandled = true;
    }
}