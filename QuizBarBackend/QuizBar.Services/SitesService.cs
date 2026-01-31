namespace QuizBar.Services;

using Microsoft.EntityFrameworkCore;
using QuizBar.Data;
using QuizBar.Services.ServiceModels.Inputs.Sites;
using QuizBar.Services.ServiceModels.Results;
using QuizBar.Services.ServiceModels.Results.Sites;
using QuizBar.Shared;

public class SitesService(ApplicationDbContext db)
{
    public async Task<ServiceResult<GetSitesServiceResult>> GetSitesAsync(GetSitesServiceInput input)
    {
        var serviceResult = new ServiceResult<GetSitesServiceResult>();

        var sitesQuery = db.Sites
            .AsQueryable();

        if (input.Filter is not null)
        {
            if (!string.IsNullOrWhiteSpace(input.UserId))
            {
                sitesQuery = sitesQuery
                    .Where(ql => ql.CreatedBy == input.UserId);
            }
        }

        var sites = await sitesQuery
            .OrderBy(q => q.Title)
            .Skip((input.Page - 1) * 10)
            .Take(10)
            .ToListAsync();

        var quizLocationsTotalItems = await db.ExceptionLogs
            .CountAsync();

        serviceResult.Result = new GetSitesServiceResult
        {
            Page = input.Page,
            TotalItems = quizLocationsTotalItems,
            Sites = sites.Select(quizLocation => new Site
            {
                Id = quizLocation.Id,
                Title = quizLocation.Title,
                Status = quizLocation.Status
            })
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

        var site = new Data.Entities.Site
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