using Microsoft.EntityFrameworkCore;
using QuizBar.Data;
using QuizBar.Data.Entities;
using QuizBar.Services.Inputs.QuizLocations;
using QuizBar.Services.Results;
using QuizBar.Services.Results.QuizLocations;
using QuizBar.Shared;

namespace QuizBar.Services;

public class QuizLocationsService(
    ApplicationDbContext db)
{
    public async Task<ServiceResult<GetQuizLocationsServiceResult>> GetQuizLocationsAsync(GetQuizLocationsServiceInput input)
    {
        var serviceResult = new ServiceResult<GetQuizLocationsServiceResult>();

        var quizLocationsQuery = db.QuizLocations
                                       .AsQueryable();

        if (input.Filter is not null)
        {
            if (!string.IsNullOrWhiteSpace(input.Filter.UserId))
            {
                quizLocationsQuery = quizLocationsQuery
                                        .Where(ql => ql.CreatedBy == input.Filter.UserId);
            }
        }

        var quizLocations = await quizLocationsQuery
                                     .Skip((input.Page - 1) * 10)
                                        .Take(10)
                                            .ToListAsync();

        var quizLocationsTotalItems = await db.ExceptionLogs
                                                  .CountAsync();

        serviceResult.Result = new GetQuizLocationsServiceResult
        {
            QuizLocations = quizLocations.Select(quizLocation => new GetQuizLocationsLocation
            {
                Id = quizLocation.Id,
                Site = quizLocation.Site,
                Status = quizLocation.Status
            }),
            Page = input.Page,
            TotalItems = quizLocationsTotalItems
        };

        return serviceResult;
    }
    public async Task<ServiceResult<CreateQuizLocationServiceResult>> CreateQuizLocationsAsync(CreateQuizLocationServiceInput input)
    {
        var serviceResult = new ServiceResult<CreateQuizLocationServiceResult>();

        var quizLocationExists = db.QuizLocations
                                       .FirstOrDefaultAsync(quizLocation => quizLocation.Site == input.Site && quizLocation.CreatedBy == input.UserId);
        
        if (quizLocationExists is not null)
        {
            serviceResult.Reason = ErrorReason.QuizEventAlreadyExists;
            return serviceResult;
        }

        var quizLocation = new QuizLocation
        {
            Id = Guid.CreateVersion7().ToString(),
            Site = input.Site,
            Status = Statuses.Active,
            CreatedOn = DateTime.UtcNow,
            CreatedBy = input.UserId
        };

        await db.QuizLocations.AddAsync(quizLocation);
        await db.SaveChangesAsync();

        serviceResult.Result = new CreateQuizLocationServiceResult
        {
            Id = quizLocation.Id
        };

        return serviceResult;
    }
    public async Task<ServiceResult<UpdateQuizLocationServiceResult>> UpdateQuizLocationsAsync(UpdateQuizLocationServiceInput input)
    {
        var serviceResult = new ServiceResult<UpdateQuizLocationServiceResult>();

        var quizLocation = await db.QuizLocations
                                       .FirstOrDefaultAsync(quizLocation => quizLocation.Site == input.Site && quizLocation.CreatedBy == input.UserId);

        if (quizLocation is null)
        {
            serviceResult.Reason = ErrorReason.QuizLocationNotFound;
            return serviceResult;
        }

        quizLocation.Site = input.Site;

        db.QuizLocations.Update(quizLocation);
        await db.SaveChangesAsync();

        serviceResult.Result = new UpdateQuizLocationServiceResult
        {
            Id = quizLocation.Id
        };

        return serviceResult;
    }
}