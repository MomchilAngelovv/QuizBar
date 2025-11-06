namespace QuizBar.Services;

using Microsoft.EntityFrameworkCore;
using QuizBar.Data;
using QuizBar.Data.Entities;
using QuizBar.Shared.ServiceModels.Inputs.ExceptionLogs;
using QuizBar.Shared.ServiceModels.Results;
using QuizBar.Shared.ServiceModels.Results.ExceptionLog;

public class ExceptionLogsService(
    ApplicationDbContext db)
{
    public async Task<ServiceResult<GetExceptionLogsServiceResult>> GetExceptionLogsAsync(GetExceptionLogsServiceInput input)
    {
        var serviceResult = new ServiceResult<GetExceptionLogsServiceResult>();

        var exceptionLogs = await db.ExceptionLogs
            .Skip((input.Page - 1) * 10)
            .Take(10)
            .ToListAsync();

        var exceptionLogsTotalItems = await db.ExceptionLogs.CountAsync();

        serviceResult.Result = new GetExceptionLogsServiceResult
        {
           Exceptions = exceptionLogs.Select(el => new GetExceptionLogsException
           {
               Id = el.Id,
               Message = el.Message,
               Path = el.Path,
               QueryString = el.QueryString,
               RouteValues = el.RouteValues,
               Headers = el.Headers,
               OccurredOn = el.OccurredOn
           }),
           Page = input.Page,
           TotalItems = exceptionLogsTotalItems
        };

        return serviceResult;
    }
    public async Task<ServiceResult<GetExceptionLogServiceResult>> GetExceptionLogAsync(GetExceptionLogServiceInput input)
    {
        var serviceResult = new ServiceResult<GetExceptionLogServiceResult>();

        var exceptionLog = await db.ExceptionLogs.FirstOrDefaultAsync(el => el.Id == input.ExceptionLogId);
        if (exceptionLog is null)
        {
            serviceResult.Reason = ErrorReason.NotFound;
            return serviceResult;
        }

        serviceResult.Result = new GetExceptionLogServiceResult 
        { 
            Id = exceptionLog.Id,
            Message = exceptionLog.Message,
            StackTrace = exceptionLog.StackTrace,
            Path = exceptionLog.Path,
            QueryString = exceptionLog.QueryString,
            RouteValues= exceptionLog.RouteValues,
            Headers = exceptionLog.Headers,
            OccurredOn = exceptionLog.OccurredOn
        };

        return serviceResult;
    }
    public async Task<ServiceResult<CreateExceptionLogServiceResult>> CreateExceptionLogAsync(CreateExceptionLogServiceInput input)
    {
        var serviceResult = new ServiceResult<CreateExceptionLogServiceResult>();

        var exceptionLog = new ExceptionLog
        {
            Id = Guid.CreateVersion7().ToString(),
            Message = input.Message,
            StackTrace = input.StackTrace,
            Path = input.Path,
            QueryString = input.QueryString,
            RouteValues = input.RouteValues,
            Headers = input.Headers,
            OccurredOn = DateTime.UtcNow
        };

        await db.ExceptionLogs.AddAsync(exceptionLog);
        await db.SaveChangesAsync();

        serviceResult.Result = new CreateExceptionLogServiceResult
        { 
            Id = exceptionLog.Id 
        };

        return serviceResult;
    }
}