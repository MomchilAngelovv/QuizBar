using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using QuizBar.Data;
using QuizBar.Data.Entities;
using System.Text;

namespace QuizBar.Api.Utilities;

public class ExceptionLogExceptionFilter(
    ApplicationDbContext db) : IAsyncExceptionFilter
{
    public async Task OnExceptionAsync(ExceptionContext context)
    {
        var request = context.HttpContext.Request;

        string query = request.QueryString.HasValue ? request.QueryString.Value : "";
        string routeValues = string.Join(", ", request.RouteValues.Select(r => $"{r.Key}={r.Value}"));
        string headers = string.Join("; ", request.Headers.Select(h => $"{h.Key}: {h.Value}"));

        string requestBody = await ReadRequestBodyAsync(request);

        var error = new ExceptionLog
        {
            Id = Guid.CreateVersion7().ToString(),
            Message = context.Exception.Message,
            StackTrace = context.Exception.StackTrace,
            Path = request.Path,
            QueryString = query,
            RouteValues = routeValues,
            Headers = headers,
            RequestBody = requestBody,
            CreatedOn = DateTime.UtcNow
        };

        await db.ExceptionLogs.AddAsync(error);
        await db.SaveChangesAsync();

        context.Result = new ObjectResult("An unexpected error occurred.") { StatusCode = 500 };
        context.ExceptionHandled = true;
    }

    private static async Task<string> ReadRequestBodyAsync(HttpRequest request)
    {
        request.EnableBuffering();

        request.Body.Position = 0;
        using var reader = new StreamReader(request.Body, Encoding.UTF8, leaveOpen: true);
        var body = await reader.ReadToEndAsync();
        request.Body.Position = 0;
        return body;
    }
}