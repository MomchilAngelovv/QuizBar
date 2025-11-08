namespace QuizBar.Services;

using Microsoft.EntityFrameworkCore;
using QuizBar.Data;
using QuizBar.Data.Entities;
using QuizBar.Shared;
using QuizBar.Shared.ServiceModels.Inputs.QuizLocations;
using QuizBar.Shared.ServiceModels.Results;
using QuizBar.Shared.ServiceModels.Results.QuizLocations;

public class SitesService(
    ApplicationDbContext db)
{
    public async Task<ServiceResult<GetSitesServiceResult>> GetSitesAsync(GetSitesServiceInput input)
    {
        var serviceResult = new ServiceResult<GetSitesServiceResult>();

        var quizLocationsQuery = db.Sites
            .AsQueryable();

        if (input.Filter is not null)
        {
            if (!string.IsNullOrWhiteSpace(input.UserId))
            {
                quizLocationsQuery = quizLocationsQuery
                    .Where(ql => ql.CreatedBy == input.UserId);
            }
        }

        var quizLocations = await quizLocationsQuery
            .OrderBy(q => q.Title)
            .Skip((input.Page - 1) * 10)
            .Take(10)
            .ToListAsync();

        var quizLocationsTotalItems = await db.ExceptionLogs
            .CountAsync();

        serviceResult.Result = new GetSitesServiceResult
        {
            QuizLocations = quizLocations.Select(quizLocation => new GetSitesSite
            {
                Id = quizLocation.Id,
                Title = quizLocation.Title,
                Status = quizLocation.Status
            }),
            Page = input.Page,
            TotalItems = quizLocationsTotalItems
        };

        return serviceResult;
    }
    public async Task<ServiceResult<CreateSiteServiceResult>> CreateSiteAsync(CreateSiteServiceInput input)
    {
        var serviceResult = new ServiceResult<CreateSiteServiceResult>();

        var siteExists = db.Sites
            .Any(quizLocation => quizLocation.Title == input.Title && quizLocation.CreatedBy == input.UserId);
        
        if (siteExists)
        {
            serviceResult.Reason = ErrorReason.SiteAlreadyExists;
            return serviceResult;
        }

        var site = new Site
        {
            Id = Guid.CreateVersion7().ToString(),
            Title = input.Title,
            Status = Statuses.Active,
            CreatedOn = DateTime.UtcNow,
            CreatedBy = input.UserId
        };

        await db.Sites.AddAsync(site);
        await db.SaveChangesAsync();

        serviceResult.Result = new CreateSiteServiceResult
        {
            Id = site.Id
        };

        return serviceResult;
    }
    public async Task<ServiceResult<UpdateSiteServiceResult>> UpdateSiteAsync(UpdateSiteServiceInput input)
    {
        var serviceResult = new ServiceResult<UpdateSiteServiceResult>();

        var site = await db.Sites
            .FirstOrDefaultAsync(site => site.Title == input.Title && site.CreatedBy == input.UserId);
        if (site is null)
        {
            serviceResult.Reason = ErrorReason.SiteNotFound;
            return serviceResult;
        }

        site.Title = input.Title;
            
        db.Sites.Update(site);
        await db.SaveChangesAsync();

        serviceResult.Result = new UpdateSiteServiceResult
        {
            Id = site.Id
        };

        return serviceResult;
    }
}