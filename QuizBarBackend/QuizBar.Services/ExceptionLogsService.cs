using Microsoft.EntityFrameworkCore;
using QuizBar.Data;
using QuizBar.Data.Entities;
using QuizBar.Services.Inputs.ExceptionLogs;
using QuizBar.Services.Results;
using QuizBar.Services.Results.ExceptionLog;

namespace QuizBar.Services;

public class ExceptionLogsService(
    ApplicationDbContext db)
{
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
}