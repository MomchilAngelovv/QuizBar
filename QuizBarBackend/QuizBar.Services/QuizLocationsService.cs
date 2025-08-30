using Microsoft.EntityFrameworkCore;
using QuizBar.Data;
using QuizBar.Services.Inputs.QuizLocations;
using QuizBar.Services.Results;
using QuizBar.Services.Results.QuizLocations;

namespace QuizBar.Services;

public class QuizLocationsService(
    ApplicationDbContext db)
{
    public async Task<ServiceResult<GetQuizLocationsServiceResult>> GetQuizLocationsAsync(QuizLocationsFilter filter = null)
    {
        var serviceResult = new ServiceResult<GetQuizLocationsServiceResult>();

        var quizLocationsQuery = db.QuizLocations.AsQueryable();

        if (filter is not null)
        {
            if (!string.IsNullOrWhiteSpace(filter.UserId))
            {
                quizLocationsQuery = quizLocationsQuery.Where(ql => ql.CreatedBy == filter.UserId);
            }
        }

        var quizLocations = await quizLocationsQuery.ToListAsync();

        serviceResult.Result = new GetQuizLocationsServiceResult
        {
            QuizLocations = quizLocations.Select(ql => new GetQuizLocationsLocation
            {
                Id = ql.Id,
                Site = ql.Site,
                Status = ql.Status
            })
        };

        return serviceResult;
    }
}